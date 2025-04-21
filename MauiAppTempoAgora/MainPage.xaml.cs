using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Networking;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private void AtualizarIconeConexao()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                imgConexao.Source = "wifi_on.png"; 
            }
            else
            {
                imgConexao.Source = "wifi_off.png";
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {

                AtualizarIconeConexao();

                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Sem conexão", "Você está sem acesso à internet. Conecte-se para ver a previsão do tempo.", "OK");
                    return;
                }



                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if(t != null)
                    {
                        string dados_previsão = "";

                        dados_previsão = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n"+
                                         $"Descrição: {t.description} \n"+
                                         $"Visibilidade: {t.visibility} \n"+
                                         $"Velocidade: {t.speed} \n";

                        lbl_res.Text = dados_previsão;

                    }else 
                    {
                        lbl_res.Text = "Cidade Não encontrada, sem dados de previsão";


                    }


                }
                else
                {

                    lbl_res.Text = "Preencha a cidade de sua escolha:";


                }

                

            }
            catch (Exception ex)
            {

                await DisplayAlert("Ops", ex.Message, "OK");

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
            AtualizarIconeConexao();
        }

    }

}
