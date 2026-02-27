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
        double totalAmount = 0;
        int frequentRenterPoints = 0;
        string result = "Rental Record for " + getName() + "\n";

        foreach (Rental each in _rentals) {
            double thisAmount = 0;

            // 一行ごとに金額を計算
            thisAmount = amountFor(each);

            // レンタルポイントを加算
            frequentRenterPoints++;

            //新作を2日以上借りた場合はボーナスポイント
            if ((each.getMovie().getPriceCode() == Movie.NEW_RELEASE) &&
                each.getDaysRented() > 1) frequentRenterPoints++;

            //この貸し出しに関する数値の表示
            result += "\t" + each.getMovie().getTitle() + "\t" +
                      thisAmount.ToString() + "\n";
            totalAmount += thisAmount;
        }
        //フッタ部分の追加
        result += "Amount owed is " + totalAmount + "\n";
        result += "You earned " + frequentRenterPoints
                + " frequent renter points";

        return result;
    }

    private double amountFor(Rental aRental)
    {
        return aRental.getCharge();
    }
}

