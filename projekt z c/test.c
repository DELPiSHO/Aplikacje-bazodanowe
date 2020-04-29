#include <stdlib.h>
#include <libpq-fe.h>
#include <unistd.h>

void doSQL(PGconn *conn, char *command)
{
  PGresult *result;

  printf("%s\n", command);

  result = PQexec(conn, command);
  printf("status is     : %s\n", PQresStatus(PQresultStatus(result)));
  printf("#rows affected: %s\n", PQcmdTuples(result));
  printf("result message: %s\n", PQresultErrorMessage(result));

  switch(PQresultStatus(result)) {
  case PGRES_TUPLES_OK:
    {
      int n = 0, m = 0;
      int nrows   = PQntuples(result);
      int nfields = PQnfields(result);
      printf("number of rows returned   = %d\n", nrows);
      printf("number of fields returned = %d\n", nfields);
      for(m = 0; m < nrows; m++) {
	for(n = 0; n < nfields; n++)
	  printf(" %s = %s", PQfname(result, n),PQgetvalue(result,m,n));
	printf("\n");
      }
    }
  }
  PQclear(result);
}

void insert(PGconn *conn){

	char imie[20],nazwisko[20],stanowisko[50];
	double dodatek;
	int gabinet;
	printf("__________________________");
	printf("\nDodawanie danych do tabeli\n");
	printf("--------------------------\n");
	printf("Podaj imie: ");
	scanf("%s",imie);

	printf("Podaj nazwisko: ");
	scanf("%s",nazwisko);

	printf("Podaj wysokość dodatku: ");
	scanf("%lf",&dodatek);

	printf("Podaj gabinet: ");
	scanf("%i",&gabinet);

	printf("Podaj stanowisko: ");
	scanf("%s",stanowisko);
	
	char command[200];
	
	sprintf(command,"INSERT INTO osoba(imie, nazwisko, data_dol, dodatek, nr_gabinetu, 		stanowisko) values('%s','%s', current_date, %lf, %i,'%s')",imie,nazwisko,dodatek,gabinet,stanowisko);

	doSQL(conn, command);
 	printf("\n\n");
	doSQL(conn, "SELECT * FROM osoba"); 

}

void update(PGconn *conn){
	
	int a,b;
	char command[100],pom[20],dane[20];
	printf("Podaj id uzytkownika ktorego dane chcesz zmodyfikowac : ");
	scanf("%i",&a);
	printf("\n");
	sprintf(command,"SELECT * FROM osoba WHERE id=%i",a);
	doSQL(conn,command);
	
	printf("\nKtory rekord ma byc zmieniony ? ");
	printf("\n1 - Imie\n2 - Nazwisko\n3 - Data_dol\n4 - dodatek\n5- Nr_gabinetu\n6 - Stanowisko \n");
	scanf("%i",&b);
	printf("Podaj nową wartość rekordu: ");
	scanf("%s",dane);
	
	if(b==1){sprintf(pom,"imie");}
	else if(b==2){sprintf(pom,"nazwisko");}
	else if(b==3){sprintf(pom,"data_dol");}
	else if(b==4){sprintf(pom,"dodatek");}
	else if(b==5){sprintf(pom,"nr_gabinetu");}
	else if(b==6){sprintf(pom,"stanowisko");}
	
	sprintf(command,"UPDATE osoba SET %s = '%s' WHERE id = %i",pom,dane,a);
	doSQL(conn,command);

	sprintf(command,"SELECT * FROM osoba WHERE id=%i",a);
	doSQL(conn,command);
	printf("\n\n");
}

void html(PGconn *conn)/*{

  FILE *file;
  char odp;
  PGresult *result;
  
  {
    result = PQexec(conn, "SELECT * FROM osoba");
    if(PQresultStatus(result)==PGRES_TUPLES_OK)
    {
      PQprintOpt pqp;
      pqp.header = 1;
      pqp.align = 1;
      pqp.html3 = 1;
      pqp.expanded = 0;
      pqp.pager = 0;
      pqp.fieldSep = "   ";
      pqp.tableOpt = "align=center";
      pqp.caption = "Lista pracownikow";
      pqp.fieldName = NULL;
if ((file = fopen("baza.html","w")) == NULL)
        printf("File opened\n");
     else{
    fprintf(file,"<HTML><HEAD></HEAD><BODY>\n");
      PQprint(file, result, &pqp);
      fprintf(file,"</BODY></HTML>\n");
        }
     fclose(file);   
    }}}

	PGresult *result;
	FILE *fp = fopen("baza.html","w"); 
   	
   if (fp==NULL) {
     printf ("Nie mogę otworzyć pliku test.txt do zapisu!\n");
     exit(1);
     }
  
    result = PQexec(conn, "SELECT * FROM osoba");
    if(PQresultStatus(result)==PGRES_TUPLES_OK) 
    { 
      PQprintOpt web;
      web.header = 1;
      web.align = 1;
      web.html3 = 1;
      web.expanded = 0;
      web.pager = 0;
      web.fieldSep = "";
      web.tableOpt = "align=center";
      web.caption = "Bingham Customer List";
      web.fieldName = NULL;
      printf("<HTML><HEAD></HEAD><BODY>\n");
      PQprint(stdout, result, &web);
      printf("</BODY></HTML>\n");
    }


   fclose (fp); 
}
*/
{
FILE *file;

  PGresult *result;
  if(PQstatus(conn) == CONNECTION_OK)
  {
    result = PQexec(conn, "SELECT * FROM osoba");
    if(PQresultStatus(result)==PGRES_TUPLES_OK)
    {
      PQprintOpt pqp;
      pqp.header = 1;
      pqp.align = 1;
      pqp.html3 = 1;
      pqp.expanded = 0;
      pqp.pager = 1;
      pqp.fieldSep = "       ";
      pqp.tableOpt = "align=center";
      pqp.caption = "Lista pracownikow";
      pqp.fieldName = NULL;

if ((file = fopen("baza.html","w")) == NULL)
        printf("Plik stworzony\n");
     else{
    fprintf(file,"<HTML><HEAD></HEAD><BODY>\n");
      PQprint(file, result, &pqp);
      fprintf(file,"</BODY></HTML>\n");
        }
     fclose(file);
      }
    }
  }



int main()
{
	int a=4;
  PGresult *result;
  PGconn   *conn;
	char user[20],*password,logowanie[20];
	printf("Logowanie\n----------\n");
	printf("Uzytkownik:");
	scanf("%s",user);
	password = getpass("Haslo");
	
	sprintf(logowanie,"host=localhost port=5432 dbname=projektdb user=%s password=%s",user,password);

	
	
  conn = PQconnectdb(logowanie);
  if(PQstatus(conn) == CONNECTION_OK) {
    printf("connection made\n");

    doSQL(conn, "DROP TABLE osoba");
    doSQL(conn, "CREATE TABLE osoba(id SERIAL PRIMARY KEY, imie VARCHAR(20), nazwisko VARCHAR(20), data_dol DATE, dodatek 		DECIMAL(7,2), nr_gabinetu INTEGER, stanowisko VARCHAR(50))");

   	doSQL(conn, "INSERT INTO osoba(imie, nazwisko, data_dol, dodatek, nr_gabinetu, stanowisko) values('Kuba','Roztocki', ('2013-01-01'), 1500.50,  12, 	'Recepcjonista'),('Dawid','Dawidowski', ('2014-02-02'), 1500.50, 12,  	'Recepcjonista'),         ('Henryk','Zareba', ('2015-03-03'), 1500.50, 12, 	'Recepcjonista'),       ('Marcin','Rolewski', ('2016-04-04'), 1500.50, 12, 	'Recepcjonista'),        ('Piotr','Zablocki', ('2016-04-04'), 1500.50, 12, 	'Recepcjonista'),         ('Kamil','Staszak', ('2016-04-04'), 1500.50, 12, 	'Recepcjonista'),       ('Maciej','Krajnik', ('2016-04-04'), 1500.50, 12, 	'Recepcjonista'),          ('Marek','Daleca', ('2016-04-04'), 1500.50, 12, 	'Recepcjonista'),       ('Michal','Bielecki', ('2016-04-04'), 1500.50, 12, 	'Recepcjonista'),       ('Patryk','Szarecki', ('2016-04-04'), 1500.50, 12, 	'Recepcjonista')");
    
	doSQL(conn, "SELECT * FROM osoba");
    	doSQL(conn, "SELECT * FROM osoba ORDER BY id ASC"); 
  }
  else
    printf("connection failed: %s\n", PQerrorMessage(conn));
 
	while (a!=0){	
		  printf("\n Czekam na kolejne polecenie : \n");
		  printf("____________________________________________________________________________________________");
		printf("\n|| 1 - Dodaj do tabeli || 2 - Modfikacja rekordow || 3 - Stworz plik HTML ||  0 - Zakoncz ||\n");
printf("--------------------------------------------------------------------------------------------\n");
		scanf("%i",&a);
		if(a==1){insert(conn);}
		else if(a==2){update(conn);}
		else if(a==3){html(conn);}
	}
  PQfinish(conn);
  return EXIT_SUCCESS;
}





