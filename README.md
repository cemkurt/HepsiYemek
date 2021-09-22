# HepsiYemek Setup

##### Proje ayağa kalkması için HepsiYemek.APIs projesi içindeki docker-compose.yml up ayağa kalkmalıdır. Redis içindeki port ve şifreler appsetting.json içersinde eşleştirilmiştir. docker ayağa kalktıktan sonra başka bi ayar yapmaya gerek kalmayacaktır. Seed data için postman collectionı paylaşıyorum yine aynı proje içersinde. İlk önce category eklenmeli ve responsedan dönen category objesinin id değeri, product requeste eklenmeli ve yeni product bu şekilde eklenmeli. Postman dışında aynı işlemler swaggerlada gerçekleştirilebilir. Request ve responselar için automapper kullanıldı. 

##### Proje Design mimari olarak unit of work kullanıldı. Repository erişimleri istekleri önce buraya düşer ve oradan ilgili T tipindeki objeye yönlendirilir. Domain katmanı için HepsiYemek.Models katmanı, db işlemleri için HepsiYemek.DataService katmanı kullanıldı. UI içinde HepsiYemek.Apis mvc api projesi kullanıldı. 




#### Redis için gerekli paketler
###### StackExchange.Redis.Extensions.Core
###### StackExchange.Redis.Extensions.AspNetCore
###### StackExchange.Redis.Extensions.Newtonsoft
