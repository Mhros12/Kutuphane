using System.ComponentModel.DataAnnotations;

namespace kutuphane.Models
{
    public class RegistrationModel
    {
        [Key]
        public Guid RegId { get; set; }
        public string NameSurname { get; set; }
        public String BookName { get; set; }
         //id lerin int mi id değerlerini int değil de Guid tutman daha doğru olurdu. sql de uniqueidentifier olarak geçer 
        //int id olarak belirlerken 2 tane aynı id olabiliyo mu Olmaz ama id ler genel olarak guid olarak tutulur büyük projelerde. anladım 
        //şİMDİ bu anlattıklarımı koduna uygulamaya başla bakalım. Türkçe bir şey görmek istemiyorum tamamdır düzeltirim tüm değişkenleri 
        //Başka soracağın yoksa ben kaçıyorum yok teşekkürler kolay gelsin bakalım sağol
    }
}
