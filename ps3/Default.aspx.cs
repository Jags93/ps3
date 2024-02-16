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
                                                   <a href='Dettagli.aspx?idItem={item.idItem}' class='btn btn-primary mt-4'>Dettagli</a>
                                                </div>   
                                            </div>
                                        </div>";
                    containerProducts.InnerHtml += cardHtml;
                }

         
            
            }



        }

        public class Product
        {
            public int idItem { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Image { get; set; }

            public Product(int id, string name, decimal price, string image)
            {
                idItem = id;
                Name = name;
                Price = price;
                Image = image;
            }
        }

        //creare la lista dei prodotti ed eventali prezzi
        public List<Product> Products 
        {
            
            get
            {
                if (Session["Catalogo"] == null)
                {
                    Session["Catalogo"] = new List<Product>
                        {
                        new Product(1,"iPhone", 1400, "https://flaminiacomputer.it/wp-content/uploads/2022/11/iphone-14-finish-select-202209-6-1inch_AV2_GEO_EMEA.jpeg"),
                        new Product(2,"Macbook", 1800, "https://5.imimg.com/data5/SELLER/Default/2021/2/KQ/EU/PX/122095513/apple-laptop-i7.PNG"),
                        new Product(3,"Iwatch", 419, "https://store.storeimages.cdn-apple.com/1/as-images.apple.com/is/ML743_VW_34FR+watch-case-41-stainless-gold-s9_VW_34FR+watch-face-41-stainless-gold-s9_VW_34FR?wid=752&hei=720&bgc=fafafa&trim=1&fmt=p-jpg&qlt=80&.v=WmtqemNXbzJrSUhnaHJ5ZlJhY3NZdDlWL3ZkbjVDbFV2QXF2czAxWWJXYkRsaU1LbHJSSUdkbzU1bnVMdkRDT2VPYzl6QnR0dWVTR2N5RXJHSkhsQXV3Z0FFbHJOMTRaU0lOb2RTdXdheXhQQ2k0c3ptdHNUTktGQnRKZVhpNWtEbHJhREVkUlF5L0c3emxEY0c2QUJtdHpZVGZEajdNRW5XSVpDZ0FDVDlSc29VdkFQbWg1b0NDTTJLejlERVlKYkJZTnJwOEl1ajIwenExd0JnT0ZLb2VKN0llWWxoMUdoTXNrS00vWmQyOD0"),
                        new Product(4,"Mac Pro", 15000, "https://celltronics.vteximg.com.br/arquivos/ids/157762-410-410/mac-pro-A1289-4.jpg"),

                    };
                }
                return (List<Product>)Session["Catalogo"];
            }
        }

    }
}