using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.ComponentModel;

public class PersonRegistration {
    int id;
    string firstName, lastName;
    int age;
    string email;
    DateTime arrivalDate;
    public PersonRegistration(int id) { this.id = id; } 
    public PersonRegistration() {
    }
    public int Id { get { return id; } set { id = value; } }
    public string FirstName { get { return firstName; } set { firstName = value; } }
    public string LastName { get { return lastName; } set { lastName = value; } }
    public string Email { get { return email; } set { email = value; } }
    public int Age { get { return age; } set { age = value; } }
    public DateTime ArrivalDate { get { return arrivalDate; } set { arrivalDate = value; } }

    internal void Assign(PersonRegistration data) {
        this.firstName = data.FirstName;
        this.lastName = data.LastName;
        this.email = data.Email;
        this.age = data.Age;
        this.arrivalDate = data.ArrivalDate;
    }
}
public class MyPersonProvider {
    HttpSessionState Session { get { return HttpContext.Current.Session; } }

    public BindingList<PersonRegistration> GetList() {
        BindingList<PersonRegistration> res = Session["PersonRegistration"] as BindingList<PersonRegistration>;
        if(res == null) res = CreateData();
        Session["PersonRegistration"] = res;
        return res;
    }

    BindingList<PersonRegistration> CreateData() {
        BindingList<PersonRegistration> res = new BindingList<PersonRegistration>();
        res.Add(AddPersonRegistration(res.Count + 1, "Andrew", "Fuller", 42, "andrew.fuller@devexpress.com", DateTime.Today.AddDays(-32)));
        res.Add(AddPersonRegistration(res.Count + 1, "Nancy", "Davolio", 34, "nancy.davolio@devexpress.com", DateTime.Today.AddDays(4)));
        res.Add(AddPersonRegistration(res.Count + 1, "Margaret", "Peackop", 48, "margaret.peackop.devexpress.com", DateTime.Today.AddDays(6)));
        res.Add(AddPersonRegistration(res.Count + 1, "Robert", "K", 29, "robert.king@devexpress.com", DateTime.Today.AddDays(5)));
        res.Add(AddPersonRegistration(res.Count + 1, "Anne", "Dodsworth", 17, "anne.dodsworth@devexpress.com", DateTime.Today.AddDays(4)));
        return res;
    }
    PersonRegistration AddPersonRegistration(int id, string firstName, string lastName, int age, string email, DateTime arrivalDate) {
        PersonRegistration data = new PersonRegistration(id);
        data.FirstName = firstName;
        data.LastName = lastName;
        data.Age = age;
        data.Email = email;
        data.ArrivalDate = arrivalDate;
        return data;
    }
    public void Insert(PersonRegistration data) {
        GetList().Add(data);
        data.Id = GetList().Count;
    }
    public void Update(PersonRegistration data) {
        BindingList<PersonRegistration> list = GetList();
        foreach(PersonRegistration f in list) {
            if(f.Id == data.Id) {
                f.Assign(data);
                return;
            }
        }
    }
}



