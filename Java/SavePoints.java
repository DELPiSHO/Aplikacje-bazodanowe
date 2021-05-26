//package file;

import java.sql.*;

public class SavePoints {

	/**
	 * @param args
	 */
	public static void main(String[] args) throws ClassNotFoundException
	{
		Connection connection=null;
		String[] imiona = { "Ala", "Basia", "Dusia", "Gienia", "Kasia" };
		Savepoint sp;
		boolean success = false;
		try
		{
			 Class.forName("org.postgresql.Driver");
             
                connection = DriverManager.getConnection("jdbc:postgresql://sigma.ug.edu.pl/nazwa_bazy","user","haslo");
		connection.setAutoCommit( false );
			Statement stmt = connection.createStatement();
			stmt.executeUpdate( "delete from Friends" );
			stmt.executeUpdate( "insert into Friends values("+1+", '"+imiona[0]+"')" );
			stmt.executeUpdate( "insert into Friends values("+2+", '"+imiona[1]+"')" );
			sp = connection.setSavepoint( "dwa_pierwsze" );
			stmt.executeUpdate( "insert into Friends values("+3+", '"+imiona[2]+"')" );
			stmt.executeUpdate( "insert into Friends values("+4+", '"+imiona[3]+"')" );
			connection.rollback( sp );
			stmt.executeUpdate( "insert into Friends values("+5+", '"+imiona[4]+"')" );
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
}
