using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ps3
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Controlla se la pagina viene caricata per la prima volta e non come risultato di un postback.
            if (!IsPostBack)
            {
                foreach (Product item in Products)
                {
                    string cardHtml = $@"
                                        <div class='card col border'>
                                            <img src='{item.Image}' class='card-img-top' alt='{item.Name}' style='max-height:200px;object-fit:contain'>

                                            <div class='card-body d-flex flex-column justify-content-between mt-3'>                                               
                                                <div>    
                                                    <h5 class='card-title'>{item.Name}</h5>
                                                    <p class='card-text'>Prezzo: {item.Price}</p>
                                                </div>  
                                                
                                                <div>
                                                   <a href='Dettagli.aspx?idItem={item.IdItem}' class='btn btn-primary mt-4'>Dettagli</a>
                                                </div>   
                                            </div>
                                        </div>";
                    containerProducts.InnerHtml += cardHtml;
                }

         
            
            }



        }
        // Definisce la classe 'Product' che rappresenta la struttura di un prodotto.
        public class Product
        {
            public int IdItem { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Image { get; set; }

            // Costruttore per creare un'istanza di 'Product' con specifici valori delle proprietà.
            public Product(int id, string name, decimal price, string image)
            {
                IdItem = id;
                Name = name;
                Price = price;
                Image = image;
            }
        }



        // Proprietà 'Products' che restituisce una lista di prodotti.
        // Se non esiste già una lista nella sessione, ne crea una nuova e la memorizza nella sessione.
        public List<Product> Products 
        {
            
            get
            {
                if (Session["Catalogo"] == null)
                {
                    Session["Catalogo"] = new List<Product>
                        {
                        new Product(1,"iPhone 15 Pro", 1400, "https://cdn.idealo.com/folder/Product/203248/7/203248711/s10_produktbild_gross/apple-iphone-15-pro-128gb-blue-titanium.jpg"),
                        new Product(2,"Macbook Pro m3", 3000, "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/mbp14-m3-max-pro-spaceblack-select-202310?wid=904&hei=840&fmt=jpeg&qlt=90&.v=1697230830118"),
                        new Product(3,"Iwatch", 419, "https://store.storeimages.cdn-apple.com/1/as-images.apple.com/is/ML743_VW_34FR+watch-case-41-stainless-gold-s9_VW_34FR+watch-face-41-stainless-gold-s9_VW_34FR?wid=752&hei=720&bgc=fafafa&trim=1&fmt=p-jpg&qlt=80&.v=WmtqemNXbzJrSUhnaHJ5ZlJhY3NZdDlWL3ZkbjVDbFV2QXF2czAxWWJXYkRsaU1LbHJSSUdkbzU1bnVMdkRDT2VPYzl6QnR0dWVTR2N5RXJHSkhsQXV3Z0FFbHJOMTRaU0lOb2RTdXdheXhQQ2k0c3ptdHNUTktGQnRKZVhpNWtEbHJhREVkUlF5L0c3emxEY0c2QUJtdHpZVGZEajdNRW5XSVpDZ0FDVDlSc29VdkFQbWg1b0NDTTJLejlERVlKYkJZTnJwOEl1ajIwenExd0JnT0ZLb2VKN0llWWxoMUdoTXNrS00vWmQyOD0"),
                        new Product(4,"Mac Pro", 15000, "https://celltronics.vteximg.com.br/arquivos/ids/157762-410-410/mac-pro-A1289-4.jpg"),

                    };
                }
                return (List<Product>)Session["Catalogo"];
            }
        }

    }
}