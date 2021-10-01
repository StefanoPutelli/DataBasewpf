using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

enum sex { male, female , other };

namespace DataBasewpf
{
    public partial class MainWindow : Window
    {
        Collection list = new Collection();
        public MainWindow()
        {
            InitializeComponent();
            refreshTextBox();
            Ordina.SelectedItem = "1";
            
        }
        public void Cerca_Click(object sender, RoutedEventArgs e)
        {
            
            switch(Ordina.SelectedIndex.ToString())
            {
                case "0":
                    if (!string.IsNullOrWhiteSpace(UserName.Text) && !lstNames.Items.Contains(UserName.Text))
                    {
                        var collection = list.getCollection();
                        var filter = Builders<User>.Filter.Eq("username", UserName.Text);
                        lstNames.Items.Clear();
                        using (var cursor = collection.Find(filter).ToCursor())
                        {
                            while (cursor.MoveNext())
                            {
                                foreach (var doc in cursor.Current)
                                {
                                    lstNames.Items.Add(doc.ToString());
                                }
                            }
                        }
                    }
                    else
                        refreshTextBox();
                    break;
                case "1":
                    if (!string.IsNullOrWhiteSpace(Eta.Text) && !lstNames.Items.Contains(Eta.Text))
                    {
                        var collection = list.getCollection();
                        var filter = Builders<User>.Filter.Eq("eta", Eta.Text);
                        lstNames.Items.Clear();
                        using (var cursor = collection.Find(filter).ToCursor())
                        {
                            while (cursor.MoveNext())
                            {
                                foreach (var doc in cursor.Current)
                                {
                                    lstNames.Items.Add(doc.ToString());
                                }
                            }
                        }
                    }
                    else
                        refreshTextBox();
                    break;
            }
            
            UserName.Clear();
            Eta.Clear();
        }
        public void Crea_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserCreate.Text) && !lstNames.Items.Contains(UserCreate.Text)  && !string.IsNullOrWhiteSpace(PasswordCreate.Text) && !string.IsNullOrWhiteSpace(AgeCreate.Text) && sex.SelectedItem != null)
            {
                
                if (sex.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Maschio")
                {
                    User tmp = createUser(UserCreate.Text, PasswordCreate.Text, AgeCreate.Text, global::sex.male);
                    list.addUser(tmp);
                    refreshTextBox();
                }
                else if (sex.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Femmina")
                {
                    User tmp = createUser(UserCreate.Text, PasswordCreate.Text, AgeCreate.Text, global::sex.female);
                    list.addUser(tmp);
                    refreshTextBox();
                }
                else if (sex.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Altro")
                {
                    User tmp = createUser(UserCreate.Text, PasswordCreate.Text, AgeCreate.Text, global::sex.other);
                    list.addUser(tmp);
                    refreshTextBox();
                    
                }
                UserCreate.Clear();
                PasswordCreate.Clear();
                AgeCreate.Clear();
            }
        }
        public void Remove_Click(object sender, RoutedEventArgs e)
        {
            var collection = list.getCollection();
            var filter = Builders<User>.Filter.Empty;
            using (var cursor = collection.Find(filter).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        if (lstNames.SelectedItem.ToString() == doc.ToString())
                        {
                            collection.DeleteOne(Builders<User>.Filter.Eq("Id", doc.Id));
                        }
                    }
                }
            }
            refreshTextBox();
        }
            static User createUser(string username_, string password_, string eta_, sex sex_)
        {
            var user = new User
            {
                username = username_,
                password = password_,
                eta = eta_,
                sex_ = sex_
            };
            return user;
        }

        void refreshTextBox()
        {
            lstNames.Items.Clear();
            var collection = list.getCollection();
            var filter = Builders<User>.Filter.Empty;
            using (var cursor = collection.Find(filter).ToCursor())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        lstNames.Items.Add(doc);
                    }
                }
            }
            

        }

    }
}
