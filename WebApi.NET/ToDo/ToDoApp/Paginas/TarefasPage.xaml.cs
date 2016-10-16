using System;
using System.Collections.Generic;
using ToDoApp.Logic;
using ToDoApp.Model;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDoApp.Paginas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TarefasPage : Page
    {

        private List<Tarefa> lista = new List<Tarefa>();
        public TarefasPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lista = await TarefasRequestApi.ListarAsync();

                lstDados.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                var msg = TratarException.ErrorMessage(ex);

                var dialog = new MessageDialog(msg, "Atenção!");

                await dialog.ShowAsync();

            }
        }

        private async void lstDados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var tarefa = listBox.SelectedValue as Tarefa;

            Frame.Navigate(typeof(NovaTarefaPage), tarefa);

        }
    }
}