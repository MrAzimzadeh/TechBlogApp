namespace WebApp.Models
{
    public class Advertisement
    {
        public int Id { get; set; } //
        public int Name { get; set; }
        public int SizeX { get; set; }//
        public int SizeY { get; set; }//
        public decimal Price { get; set; }//
        public decimal Rate { get; set; }//
        //sonradan yazilan
        public int View { get; set; }//
        public int Click { get; set; }  //
        //
        public string PhotoUrl { get; set; }//
        public string DirectionAddress { get; set; }//
        public DateTime CreatedDate { get; set; }//

    }
}
