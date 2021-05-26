//package file;

/*
Przygotowanie bazy danych:
 
drop table osoba;
create table osoba ( id int primary key, imie varchar(20), nazwisko varchar(20), data_ur datetime, pensja decimal(10, 2) )

begin
  delete from osoba;
  insert into osoba values( 1, 'Jan', 'Kowalski', '1976-12-24', 1500.00 );
  insert into osoba values( 2, 'Jerzy', 'Nowak', '1974-11-29', 1200.00 );
  insert into osoba values( 3, 'Micha³', 'Malinowski', '1966-10-21', 2800.00 );
end;

select * from osoba;
 */

import java.sql.*;
import java.util.Calendar;
import java.util.GregorianCalendar;

public class ManipulacjaDanymi
{
	public static void main(String[] args) throws ClassNotFoundException
	{
		Connection connection=null;
		Statement stmt=null;
		try
		{
			Class.forName("org.postgresql.Driver");

         		connection = DriverManager.getConnection("jdbc:postgresql://sigma.ug.edu.pl/nazwa_bazy","user","haslo");
			connection.setTransactionIsolation( Connection.TRANSACTION_READ_UNCOMMITTED );
            		connection.setAutoCommit( false );
			String qs = "select id, imie, nazwisko, data_ur, pensja from osoba order by id";
			stmt = connection.createStatement( ResultSet.TYPE_SCROLL_SENSITIVE, ResultSet.CONCUR_UPDATABLE );
			ResultSet rs = stmt.executeQuery(qs);
			rs.absolute(1);
			
			// INSERT
			rs.moveToInsertRow();
			rs.updateInt( "ID", 22);
			rs.updateString( "IMIE", "Alojzy" );
			rs.updateString( "NAZWISKO", "Dachowiec" );
			rs.updateDate( "DATA_UR", new Date( ( new GregorianCalendar( 1964, Calendar.APRIL, 12 ) ).getTimeInMillis() ) );
			rs.updateDouble( "PENSJA", 2400.00 );
			rs.insertRow();
			connection.commit();
			// UPDATE 
			rs.moveToCurrentRow();
			rs.updateString( "NAZWISKO", "Nowakowsky" );
			rs.updateString( "IMIE", "George" );
			rs.updateDouble( "PENSJA", 2100.00 );
			//rs.cancelRowUpdates();
			rs.updateRow();
			
			// DELETE 
		    rs.relative( 1 );
			rs.deleteRow();

			rs.close();
			connection.commit();
			
			System.out.println( "Id" + "\t" + "Imiê" + "\t" + "Nazwisko" + "\t" + "Data urodzenia" + "\t" + "Pensja" );
			rs = stmt.executeQuery(qs);
			rs.afterLast(); // a co jak damy last() ?
			while ( rs.previous() )
			{
				rs.refreshRow();
				System.out.println( rs.getInt( "ID" )
					+"\t"+ rs.getString( "IMIE" )
					+"\t"+ rs.getString( "NAZWISKO" )
					+"\t"+ rs.getDate( "DATA_UR" )
					+"\t"+ rs.getDouble( "PENSJA" )	);
           	}
			rs.close();
			stmt.close();
		}
		catch (SQLException sqle)
		{ 
			sqle.printStackTrace();
			while (sqle != null)
			{
				String logMessage = "\n SQL Error: "+
					sqle.getMessage() + "\n\t\t"+
					"Error code: "+sqle.getErrorCode() + "\n\t\t"+
					"SQLState: "+sqle.getSQLState()+"\n";
				System.out.println(logMessage);
				sqle = sqle.getNextException();
			}
		}
		finally
		{
			try { if ( connection != null ) connection.close(); }
			catch ( Exception e ) { e.printStackTrace(); }
		}
	}

}
