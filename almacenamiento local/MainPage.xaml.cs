using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Almacenamiento_Local.Resources;
using System.IO.IsolatedStorage;
using System.IO;

namespace Almacenamiento_Local
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // agregar en el contructor 
            this.Loaded += MainPage_Loaded;
        }
        const string fileName = "archivo.txt";
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            // estó va en el método MainPage_Loaded
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
	            // verificamos si el archivo ya existe
	            if(isf.FileExists(fileName))
	            {
		            // el segundo parámetro inidica cómo se debe de abrir el archivo
		            using (var stream = isf.OpenFile(fileName,FileMode.Open))
		            {
			            using (var reader = new StreamReader(stream))
			            {
				            var content = reader.ReadToEnd();
				            txtName.Text = content;
				            reader.Close();
			            }
		            }
	            }	
            }

            /*
             * settings
            // -------------- esto va con el  de storageSettings
            // verificamos si la cadena existe
            if (IsolatedStorageSettings.Contains(fileName))
            {
                // si es así entonces colocamos en el textbox el string que se había guardado
                txtName.Text = IsolatedStorageSettings.ApplicationSettings[fileName].toString();
            }
            */


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // esté metodo devulver todo el almacenamiento aislado
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // verificamos si el archivo ya existe
                if (isf.FileExists(fileName))
                {
                    isf.DeleteFile(fileName);
                }
                using (var stream = isf.CreateFile(fileName))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        // escribimos en el archivo lo que está en el textbox
                        writer.WriteLine(txtName.Text);
                        writer.Close();
                    }
                }
            }

            /*
            // guardar settings
            // ------------ esto va en el evento click del botón ------------
            // verificamos si el string ya existe
            if (IsolatedStorageSettings.ApplicationSettings.Contains(fileName))
            {
                // la removemos
                IsolatedStorageSettings.ApplicationSettings.Remove(fileName);
            }

            // agregamso el dato
            IsolatedStorageSettings.ApplicationSettings.Add(fileName, txtName.Text);
            // también se puede agregar así
            //IsolatedStorageSettings.ApplicationSettings[fileName] = txtName.Text;
            */
        }

       


    }
}