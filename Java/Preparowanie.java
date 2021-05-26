//package file;

/*
Przygotowanie bazy danych:
 
drop table osoba;
create table osoba ( id int primary key, imie varchar(20), nazwisko varchar(20), data_ur datetime, pensja decimal(10, 2) )

select * from osoba;
*/

import java.sql.*;
import java.util.Calendar;
import java.util.GregorianCalendar;

public class Preparowanie
{
	public static void main(String[] args) throws ClassNotFoundException
	{
		Connection connection=null;
		try
		{
			  Class.forName("org.psql.Driver");
              
               connection = DriverManager.getConnection("jdbc:postgresql://sigma.ug.edu.pl/nazwa_bazy","user","haslo");
			PreparedStatement pstmt = connection.prepareStatement( "insert into osoba values( ?, ?, ?, ?, ? )");
			Statement stmt = connection.createStatement();
			stmt.executeUpdate( "delete from osoba" );
			stmt.close();
			for ( int i=0; i<20; i++ )
			{
				pstmt.setInt( 1, i+1 );
				pstmt.setString( 2, "Jan"+(i+1) );
				pstmt.setString( 3, "Kowalski"+(i+1) );
				pstmt.setDate( 4, new Date( ( new GregorianCalendar( 1964, Calendar.APRIL, 12 ) ).getTimeInMillis() ) );
				pstmt.setDouble( 5, 1000.0+i*20.0 );
				pstmt.executeUpdate();
			}
			pstmt.close();
			System.out.println( "Gotowe" );
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
