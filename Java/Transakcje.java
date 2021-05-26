//package file;

/*
 *-- Chcemy, ¿eby bud¿et dla danego dzia³u to by³a suma pensji pracowników

create table dzial( dzialid int primary key, nazwa varchar(20), budzet decimal(10,2) );
create table pracownik( pracownikid int primary key, dzialid int references dzial(dzialid), nazwisko varchar(20), imie varchar(20), pensja decimal(10,2) );

begin
  insert into dzial values( 1, 'IT', 4000.00 );
  insert into dzial values( 2, 'HR', 0.00 );
  insert into pracownik values( 1, 1, 'Kowalski', 'Jakub', 1500.00 );
  insert into pracownik values( 2, 1, 'Nowak', 'Jerzy', 2500.00 );
end;

select * from pracownik;
select * from dzial;
select dzialid, sum(pensja) suma from pracownik where dzialid=(select max(dzialid) from pracownik where pracownikid=1) group by dzialid;
 */

import java.sql.*;

public class Transakcje
{
	static void zmienPensje( int pracownik, double nowaPensja ) throws ClassNotFoundException
	{
		Connection connection=null;
		boolean success = false;
		try
		{
			 Class.forName("org.postgresql.Driver");
  			connection = DriverManager.getConnection("jdbc:postgresql://sigma.ug.edu.pl/nazwa_bazy","user","haslo");
		
            		connection.setAutoCommit( false );
			Statement stmt = connection.createStatement();
			stmt.executeUpdate( "update pracownik set pensja="+nowaPensja+" where pracownikid="+pracownik );
			ResultSet rs = stmt.executeQuery( "select dzialid, sum(pensja) suma from pracownik where dzialid=(select max(dzialid) from pracownik where pracownikid="+pracownik+") group by dzialid" );
			rs.next();
			int dzial = rs.getInt( "dzialid" );
			double budzet = rs.getDouble( "suma" );
			// Poprawiamy na dzial2 i sprawdzamy, czy wycofanie zadzia³a³o
			stmt.executeUpdate( "update dzial set budzet="+budzet+" where dzialid="+dzial );
			success = true;
		}
		catch (SQLException sqle)
		{ 
			sqle.printStackTrace();
		}
		finally
		{
			if ( connection != null )
			{
				if ( success )
				{
					try { connection.commit(); }
					catch( SQLException e )
					{
						try { connection.rollback(); }
						catch ( Exception e2 ) { e2.printStackTrace(); }						
					}
				}
				else
				{
					try { connection.rollback(); }
					catch ( Exception e ) { e.printStackTrace(); }						
				}
			}
			try { connection.close(); }
			catch ( Exception e ) { e.printStackTrace(); }
		}		
	}
	public static void main(String[] args) throws ClassNotFoundException
	{
		zmienPensje( 1, 1500.00 );
	}

}
