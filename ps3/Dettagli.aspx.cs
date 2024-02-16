using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ps3._Default;

namespace ps3
{
    public partial class Dettagli : Page
    {

        // Questo metodo viene chiamato ogni volta che la pagina viene caricata.
        protected void Page_Load(object sender, EventArgs e)
        {

            // Controlla se la pagina viene caricata per la prima volta e non a seguito di un postback.

            if (!IsPostBack)
            {
                int itemId;
                if (int.TryParse(Request.QueryString["IdItem"], out itemId))
                {
                    List<Product> products = Session["Catalogo"] as List<Product>;
                    if (products != null)
                    {
                        Product product = products.FirstOrDefault(p => p.IdItem == itemId);
                        if (product != null)
                        {
                            DisplayProductDetails(product);
                        }
                        else
                        {
                            ItemDetails.InnerHtml = "Prodotto non trovato.";
                        }
                    }
                }
                else
                {
                    ItemDetails.InnerHtml = "ID non valido.";
                }
            }
        }
        // Questo metodo aggiorna il contenuto della pagina con i dettagli del prodotto.
        private void DisplayProductDetails(Product product)
        {
            string detailsHtml = $@"
                <h2>{product.Name}</h2>
                <img src='{product.Image}' alt='Immagine di {product.Name}' style='max-width:300px;'><br>
                <p>Prezzo: {product.Price:C}</p>
                <p><a href='Carrello.aspx?action=add&id_item={product.IdItem}' class='btn btn-primary'>Aggiungi al Carrello</a></p>";

            ItemDetails.InnerHtml = detailsHtml;
        }
    }
}
