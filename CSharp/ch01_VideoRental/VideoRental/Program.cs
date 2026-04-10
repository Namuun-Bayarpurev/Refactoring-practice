using System;
using System.Collections.Generic;

public static class VideoRentalApp
{
    public static void Main(string[] args)
    {
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

        string statement = customer.statement();
        Console.WriteLine(statement);
    }
}

class Movie {
    public const int CHILDRENS = 2;
    public const int REGULAR = 0;
    public const int NEW_RELEASE = 1;

    private string _title;
    private int _priceCode;

    public Movie(string title, int priceCode) {
        this._title = title;
        this._priceCode = priceCode;
    }

    public int getPriceCode() {
        return _priceCode;
    }

    public void setPriceCode(int arg) {
        this._priceCode = arg;
    }

    public string getTitle() {
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
    
    public int getFrequentRenterPoints() {
        // 新作を2日以上借りた場合はポイントを2倍
        if ((getMovie().getPriceCode() == Movie.NEW_RELEASE) && getDaysRented() > 1) 
            return 2;
        else
            return 1;
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
    private string _name;
    private List<Rental> _rentals = new List<Rental>();

    public Customer(string name) {
        this._name = name;
    }

    public void addRental(Rental arg) {
        _rentals.Add(arg);
    }

    public string getName() {
        return _name;
    }

    public string statement() {
        string result = "Rental Record for " + getName() + "\n";

        foreach (Rental each in _rentals)
        {   //この貸し出しに関する数値の表示
            result += "\t" + each.getMovie().getTitle() + "\t" +
                      each.getCharge().ToString() + "\n";
        }
        //フッタ部分の追加
        result += "Amount owed is " + getTotalCharge() + "\n";
        result += "You earned " + getTotalFrequentRenterPoints()
                + " frequent renter points";

        return result;
    }
    
    public string htmlStatement() {
        string result = "<h1>Rental Record for <em>" + getName() + "</em></h1><p>\n";
        foreach (Rental each in _rentals) 
        {
           //この貸し出しに関する数値の表示
            result += each.getMovie().getTitle() + ": " +
                      each.getCharge().ToString() + "<br>\n";
        }
        //フッタ部分の追加
        result += "<p>You owe <em>" + getTotalCharge() + "</em><p>\n";
        result += "On this rental you earned <em>" + getTotalFrequentRenterPoints()
                + "</em> frequent renter points<p>";
        return result;
    }

    private double getTotalCharge() {
        double result = 0;
        foreach (Rental each in _rentals) {
            result += each.getCharge();
        }
        return result;
    }

    private int getTotalFrequentRenterPoints() {
        int result = 0;
        foreach (Rental each in _rentals) {
            result += each.getFrequentRenterPoints();
        }
        return result;
    }

    private double amountFor(Rental aRental)
    {
        return aRental.getCharge();
    }
}

