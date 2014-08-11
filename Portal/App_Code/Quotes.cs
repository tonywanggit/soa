using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Web.SessionState;
public class Quotes {
    static string[] names = new string[] { "SUNW", "MSFT", "INTC", "CSCO", "SIRI", "AAPL", "HOKU", "ORCL", "AMAT", "YHOO", "LVLT", "DELL", "BEAS" };
    public Quotes() {

    }
    Random random;
    Random GetRandom() {
        if(random == null) random = new Random();
        return random;
    }
    HttpSessionState Session { get { return HttpContext.Current.Session; } }
    public BindingList<Quote> LoadQuotes() {
     
        if(Session["Quotes"] == null) CreateQuotes();
        ModifyQuotes();
        return Session["Quotes"] as BindingList<Quote>;

    }

    private void ModifyQuotes() {
        BindingList<Quote> quotes = Session["Quotes"] as BindingList<Quote>;
        int count = GetRandom().Next(1, 5);
        for(int n = 0; n < count; n++) {
            int num = GetRandom().Next(0, quotes.Count);
            Quote q = quotes[num];
            q.Value = (decimal)GetRandom().Next(((int)q.PrevValue * 10) - 30, ((int)q.PrevValue * 10) + 30) / (decimal)10;
            q.Time = DateTime.Now;
        }
    }

    private void CreateQuotes() {
        BindingList<Quote> res = new BindingList<Quote>();
        foreach(string name in names) {
            Quote q = new Quote(name);
            q.Value = (decimal)GetRandom().Next(800, 2000) / (decimal)10;
            res.Add(q);
        }
        Session["Quotes"] = res;
    }
}
public class Quote {
    private string fSymbol;
    private DateTime fTime;
    private decimal fValue, fPrevValue;

    public Quote(string symbol) {
        this.fSymbol = symbol;
        this.fTime = DateTime.MinValue;
        this.fValue = 0;
        this.fPrevValue = 0;
    }
    public string Symbol { get { return fSymbol; } }
    public DateTime Time { get { return fTime; } set { fTime = value; } }
    public decimal Value { 
        get { return this.fValue; } 
        set {
            if(this.fValue == value) return;
            if(this.fPrevValue == 0) this.fPrevValue = value;
            this.fValue =value;
        }
    }
    public decimal PrevValue { get { return fPrevValue; } }
    public decimal Change {
        get {
            if(PrevValue == 0 || Value == 0 || Value == PrevValue) return 0;
            if(Value > PrevValue) {
                return (Value / PrevValue) - 1;
            } else {
                return 1 - (PrevValue / Value);
            }
        }
    }
}

