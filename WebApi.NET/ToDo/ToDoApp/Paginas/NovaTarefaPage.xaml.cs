using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ToDoApp.Logic;
using ToDoApp.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDoApp.Paginas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NovaTarefaPage : Page
    {
        private Tarefa model;

        public NovaTarefaPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            model = e.Parameter as Tarefa;
            base.OnNavigatedTo(e);
        }


        private async void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            model = new Tarefa();

            model.Id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            model.Titulo = txtTitulo.Text;
            model.Descricao = txtDescricao.Text;
            model.DataLimite = dtpData.Date.DateTime;
            model.Concluido = ckbConcluido.IsChecked.Value;
            model.Username = txtUserName.Text;

            try
            {
                if (model.Id > 0)
                {
                    await TarefasRequestApi.AlterarTarefa(model);

                    var dialog = new MessageDialog("Sua tarefa foi alterada com sucesso", "Sucesso!");

                    await dialog.ShowAsync();

                    Frame.Navigate(typeof(TarefasPage));
                }
                else
                {
                    await TarefasRequestApi.GravarTarefa(model);

                    txtCodigo.Text = "";
                    txtTitulo.Text = "";
                    txtDescricao.Text = "";
                    ckbConcluido.IsChecked = false;

                    var dialog = new MessageDialog("Sua tarefa foi criada com sucesso", "Sucesso!");
                    await dialog.ShowAsync();
                }                

                txtTitulo.Focus(FocusState.Keyboard);

            } catch (Exception ex)
            {
                var msg = TratarException.ErrorMessage(ex);

                var dialog = new MessageDialog(msg, "Atenção!");

                await dialog.ShowAsync();
            }
        

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(model == null || model.Id == 0)
            {
                txtCodigo.Visibility = Visibility.Collapsed;
                txbCodigo.Visibility = Visibility.Collapsed;

                var json = MyLocalStorage.GetFromLocalStorage("usuario");
                var usuario = JsonConvert.DeserializeObject<Usuario>(json.ToString());
                txtUserName.Text = usuario.Login;
            }
            else
            {
                txtCodigo.Text = model.Id.ToString();
                txtTitulo.Text = model.Titulo;
                txtDescricao.Text = model.Descricao;
                txtUserName.Text = model.Username;
                dtpData.Date = model.DataLimite;
                ckbConcluido.IsChecked = model.Concluido;
            }           
        }

    }
}
