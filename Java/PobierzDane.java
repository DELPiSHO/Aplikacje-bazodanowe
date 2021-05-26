//package file;

/*
Przygotowanie bazy danych:

drop table Friends;
create table Friends ( id int primary key, name varchar(20) );

begin
  insert into Friends values( 1, 'Asia' );
  insert into Friends values( 2, 'Basia' );
  insert into Friends values( 3, 'Jan' );
end;

select * from Names;
 */

import java.sql.*;

public class PobierzDane {

    /**
	 * @param args
	 */
    public static void main(String[] args) throws ClassNotFoundException {
        Connection connection = null;
        Statement stmt = null;
        try {
        	//ladujemy nasz sterownik do pamieci
            Class.forName("org.postgresql.Driver");
            //nawiazujemy polaczenie z baza danych
            connection = DriverManager.getConnection("jdbc:postgresql://sigma.ug.edu.pl/nazwa_bazy","user","haslo");
            String qs = "select id, name from friends order by id";
            //tworzymy obiekt typu statment
            stmt = connection.createStatement();
            //wykonujemy zapytanie
            ResultSet rs = stmt.executeQuery(qs);
            System.out.println("id" + "\t" + "name");
            //przetwarzamy otrzymane dane
            while (rs.next()) {
                System.out.println(rs.getInt("id") + "\t" + 
                                   rs.getString("name"));
            }
            rs.close();
            stmt.close();
        } catch (SQLException sqle) {
        	//obsLuga wyjatku
            sqle.printStackTrace();
            while (sqle != null) {
                String logMessage = 
                    "\n SQL Error: " + sqle.getMessage() + "\n\t\t" + 
                    "Error code: " + sqle.getErrorCode() + "\n\t\t" + 
                    "SQLState: " + sqle.getSQLState() + "\n";
                System.out.println(logMessage);
                sqle = sqle.getNextException();
            }
        } finally {
        	//Zawsze zamykamy polaczenie z baza danych
            try {
                if (connection != null)
                    connection.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

}
