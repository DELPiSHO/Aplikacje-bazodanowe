//package file;

import java.sql.*;

public class ModyfikujDane {

    /**
	 * @param args
	 */
    public static void main(String[] args) throws ClassNotFoundException {
        Connection connection = null;
        int ile;
        String[] imiona = { "Ala", "Basia", "Dusia", "Gienia", "Kasia" };
        try {
        	  Class.forName("org.postgresql.Driver");
             
            connection = DriverManager.getConnection("jdbc:postgresql://sigma.ug.edu.pl/nazwa_bazy","user","haslo");
            Statement stmt = connection.createStatement();
            ile = stmt.executeUpdate("delete from Friends");
            System.out.println("Iloœæ usuniêtych wierszy: " + ile);
            for (int i = 0; i < imiona.length; i++) {
                ile = 
             stmt.executeUpdate("insert into Friends values(" + (i + 1) + ", '" + imiona[i] + 
                   "')");
                System.out.println("Liczba wstawionych wierszy: " + ile);
            }
            stmt.close();
        } catch (SQLException sqle) {
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
            try {
                if (connection != null)
                    connection.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

}
