//gcc -I/usr/include/postgresql -o projekt1 projekt1.c -L/usr/lib -lpq

#include <stdio.h>
#include <stdlib.h>
#include <libpq-fe.h>
#include <string.h>
#include <unistd.h>

void doSQL(PGconn *conn, char *command);
void createInsertTable(PGconn *conn);
void insertRecord(PGconn *conn);
void deleteRecord(PGconn *conn, int id);
void modifyRecord(PGconn *conn, int id);
void toHTML(PGconn *conn);

//-------------------------------------------------------------------------------------------------------------------

void doSQL(PGconn *conn, char *command) {
  PGresult *result;

  //printf("%s\n", command);

  result = PQexec(conn, command);
  printf("status is: %s\n", PQresStatus(PQresultStatus(result)));
  printf("#rows affected: %s\n", PQcmdTuples(result));
  printf("result message: %s\n", PQresultErrorMessage(result));

  switch(PQresultStatus(result)) {
  case PGRES_TUPLES_OK:
    {
      int n = 0, m = 0;
      int nrows   = PQntuples(result);
      int nfields = PQnfields(result);
      //printf("number of rows returned   = %d\n", nrows);
      //printf("number of fields returned = %d\n", nfields);
      for(m = 0; m < nrows; m++) {
	for(n = 0; n < nfields; n++)
	  printf(" %s = %s", PQfname(result, n),PQgetvalue(result,m,n));
	printf("\n");
      }
    }
  }
  PQclear(result);
}

//-------------------------------------------------------------------------------------------------------------------

void createInsertTable(PGconn *conn) {
	doSQL(conn, "DROP TABLE pracownik");
    doSQL(conn, "CREATE TABLE pracownik(id SERIAL PRIMARY KEY, imie TEXT NOT NULL, nazwisko TEXT NOT NULL, \
    data_ur DATE NOT NULL, pesel CHAR(11) NOT NULL, pensja MONEY NOT NULL);");
    doSQL(conn, "INSERT INTO pracownik(imie, nazwisko, data_ur, pesel, pensja) VALUES\
    ('Adam', 'Mochnacki', '1995-11-24', '95112493456', 3000), ('Marian', 'Grabowski', '1985-02-17', '85021784236', 4000),\
    ('Wojciech', 'Babecki', '1984-04-11', '84041124951', 4500), ('Michał', 'Piotrowski', '1986-03-15', '86031524821', 3500),\
    ('Damian', 'Brudzinski', '1992-06-05', '92060519941', 2500), ('Andrzej', 'Piotrowski', '1982-03-12', '82031284835', 3500),\
    ('Jan', 'Nowak', '1993-06-05', '93060519348', 4500), ('Maciej', 'Grabowski', '1984-03-12', '84031284837', 3000),\
    ('Janusz', 'Polanski', '1993-06-05', '93060559144', 3500), ('Adam', 'Kibicki', '1995-09-24', '95092492455', 4000)");
}

//-------------------------------------------------------------------------------------------------------------------

void insertRecord(PGconn *conn) {
	char tmp[200], imie[50], nazwisko[50], dataUr[20], pesel[11], pensja[20];
	printf("imie: ");
	scanf("%s", imie);
	printf("nazwisko: ");
	scanf("%s", nazwisko);
	printf("dataUr: ");
	scanf("%s", dataUr);
	do {
		printf("pesel: ");
		scanf("%s", pesel);
		if(strlen(pesel) != 11) printf("Pesel musi miec 11 znakow\n");
	} while(strlen(pesel) != 11);
	printf("pensja: ");
	scanf("%s", pensja);
	sprintf(tmp, "INSERT INTO pracownik(imie, nazwisko, data_ur, pesel, pensja) \
	VALUES('%s', '%s', '%s', '%s', %s)", imie, nazwisko, dataUr, pesel, pensja);
	doSQL(conn, tmp);
}

//-------------------------------------------------------------------------------------------------------------------

void deleteRecord(PGconn *conn, int id) {
	char tmp[50];
	if(id == 0) {
		doSQL(conn, "DELETE FROM pracownik");
	}
	else {
		sprintf(tmp, "DELETE FROM pracownik WHERE id = %i", id);
		doSQL(conn, tmp);
	}
}

//-------------------------------------------------------------------------------------------------------------------

void modifyRecord(PGconn *conn, int id) {
	int choice = -1;
	char tmp[50];
	char new[30];
	printf("Modyfikuj: 1- imie; 2- nazwisko; 3- data_ur; 4- pesel; 5- pensja\n");
	scanf("%i", &choice);
	printf("Podaj nowa wartosc: ");
	scanf("%s", new);
	switch(choice) {
		case 1:
			sprintf(tmp, "UPDATE pracownik SET imie = '%s' WHERE id = %i", new, id);
			doSQL(conn, tmp);
			break;
		case 2:
			sprintf(tmp, "UPDATE pracownik SET nazwisko = '%s' WHERE id = %i", new, id);
			doSQL(conn, tmp);
			break;
		case 3:
			sprintf(tmp, "UPDATE pracownik SET data_ur = '%s' WHERE id = %i", new, id);
			doSQL(conn, tmp);
			break;
		case 4:
			if(strlen(new) == 11) {
				sprintf(tmp, "UPDATE pracownik SET pesel = '%s' WHERE id = %i", new, id);
				doSQL(conn, tmp);
			}
			else printf("Pesel musi miec 11 znakow\n");
			break;
		case 5:
			sprintf(tmp, "UPDATE pracownik SET pensja = %s WHERE id = %i", new, id);
			doSQL(conn, tmp);
			break;
	}
}

//-------------------------------------------------------------------------------------------------------------------

void toHTML(PGconn *conn) {
	FILE *pracownikHTML = fopen("pracownik.html", "w");
	PGresult *result;
	
	result = PQexec(conn, "SELECT * FROM pracownik ORDER BY id ASC");
	
	switch(PQresultStatus(result)) {
		case PGRES_TUPLES_OK:
		{
			int n = 0, m = 0;
			int nrows   = PQntuples(result);
			int nfields = PQnfields(result);
			
			fprintf(pracownikHTML, "<!DOCTYPE html>\n<html>\n<head><meta charset=""UTF-8"">\n\n<title>pracownicy</title>\n<style>\n");
			fprintf(pracownikHTML, "table, th, td {border: 1px solid black;}\n</style>\n</head>\n<body>\n<table>\n");
			
			fprintf(pracownikHTML, "<tr>\n");
			for(m = 0; m < nfields; m++) {
				fprintf(pracownikHTML, "<th>%s</th>\n", PQfname(result, m));
			}
			fprintf(pracownikHTML, "</tr>\n");
			
			for(m = 0; m < nrows; m++) {
				fprintf(pracownikHTML, "<tr>\n");
				for(n = 0; n < nfields; n++) {
					fprintf(pracownikHTML, "<th>%s</th>\n", PQgetvalue(result,m,n));
				}
				fprintf(pracownikHTML, "</tr>\n");
			}
			fprintf(pracownikHTML, "</table>\n</body>\n</html>");
		}
	}
	PQclear(result);
	
	fclose(pracownikHTML);
}

//-------------------------------------------------------------------------------------------------------------------
 
int main() {
	PGresult *result;
	PGconn   *conn;
	int choice = -1;
	int id = -1;
	char user[25];
	char *pwd;
	char tmp[100];
	
	printf("User: ");
	scanf("%s", user);
	pwd = getpass("Password: ");
	
	sprintf(tmp, "host=localhost port=5432 dbname=testdb user=%s password=%s", user, pwd);
	
	conn = PQconnectdb(tmp);
	if(PQstatus(conn) == CONNECTION_OK) {
		printf("connection made\n");
		//createInsertTable(conn);
		
		while(choice != 0) {
			printf("\n1- wyswietl tabele\n2- usun wiersz po id\n3- modyfikuj wiersz\n4- do html\n5- wstaw wiersz\n0- wyjdz\n");
			scanf("%i", &choice);
			switch(choice) {
				case 1:
					doSQL(conn, "SELECT * FROM pracownik");
					break;
				case 2:
					doSQL(conn, "SELECT * FROM pracownik");
					printf("Podaj id wiersza do usuniecia (0 usunie wszystko): ");
					scanf("%i", &id);
					deleteRecord(conn, id);
					break;
				case 3:
					doSQL(conn, "SELECT * FROM pracownik");
					printf("Podaj id wiersza do zmiany: ");
					scanf("%i", &id);
					modifyRecord(conn, id);
					break;
				case 4:
					toHTML(conn);
					break;
				case 5:
					insertRecord(conn);
					break;
			}
		}
	}
	else printf("connection failed: %s\n", PQerrorMessage(conn));

	PQfinish(conn);
	return EXIT_SUCCESS;
}

