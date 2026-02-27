import java.util.Vector;
import java.util.Enumeration;

class VideoRentalApp {
    public static void main(String[] args) {
        Movie movie1 = new Movie("The Matrix", Movie.REGULAR);
        Movie movie2 = new Movie("Avengers: Endgame", Movie.NEW_RELEASE);
        Movie movie3 = new Movie("Frozen", Movie.CHILDRENS);

        Rental rental1 = new Rental(movie1, 3);
        Rental rental2 = new Rental(movie2, 2);
        Rental rental3 = new Rental(movie3, 4);

        Customer customer = new Customer("John Doe");
        customer.addRental(rental1);
        customer.addRental(rental2);
        customer.addRental(rental3);

        String statement = customer.statement();
        System.out.println(statement);
    }
}

class Movie {
    public static final int CHILDRENS = 2;
    public static final int REGULAR = 0;
    public static final int NEW_RELEASE = 1;

    private String _title;
    private int _priceCode;

    public Movie(String title, int priceCode) {
        this._title = title;
        this._priceCode = priceCode;
    }

    public int getPriceCode() {
        return _priceCode;
    }

    public void setPriceCode(int arg) {
        this._priceCode = arg;
    }

    public String getTitle() {
        return _title;
    }
}

class Rental {
    private Movie _movie;
    private int _daysRented;

    public Rental(Movie movie, int daysRented) {
        this._movie = movie;
        this._daysRented = daysRented;
    }

    public int getDaysRented() {
        return _daysRented;
    }

    public Movie getMovie() {
        return _movie;
    }

    public double getCharge() {
        double result = 0;
        switch (getMovie().getPriceCode()) {
            case Movie.REGULAR:
                result += 2;
                if (getDaysRented() > 2)
                    result += (getDaysRented() - 2) * 1.5;
                break;
            case Movie.NEW_RELEASE:
                result += getDaysRented() * 3;
                break;
            case Movie.CHILDRENS:
                result += 1.5;
                if (getDaysRented() > 3)
                    result += (getDaysRented() - 3) * 1.5;
                break;
        }
        return result;
    }
}

class Customer {
    private String _name;
    private Vector<Rental> _rentals = new Vector<>();

    public Customer(String name) {
        this._name = name;
    }

    public void addRental(Rental arg) {
        _rentals.addElement(arg);
    }

    public String getName() {
        return _name;
    }

    public String statement() {
        double totalAmount = 0;
        int frequentRenterPoints = 0;
        Enumeration<Rental> rentals = _rentals.elements();
        String result = "Rental Record for " + getName() + "\n";

        while (rentals.hasMoreElements()) {
            double thisAmount = 0;
            Rental each = rentals.nextElement();

            // 一行ごとに金額を計算
            thisAmount = amountFor(each);
            
            // レンタルポイントを加算
            frequentRenterPoints++;

            //新作を2日以上借りた場合はボーナスポイント
            if ((each.getMovie().getPriceCode() == Movie.NEW_RELEASE) &&
                each.getDaysRented() > 1) frequentRenterPoints++;

            //この貸し出しに関する数値の表示
            result += "\t" + each.getMovie().getTitle() + "\t" +
                      String.valueOf(thisAmount) + "\n";
            totalAmount += thisAmount;
        }
        //フッタ部分の追加
        result += "Amount owed is " + totalAmount + "\n";
        result += "You earned " + frequentRenterPoints
                + " frequent renter points";

        return result;
    }

    private double amountFor(Rental aRental) {
        return aRental.getCharge();
    }
}
