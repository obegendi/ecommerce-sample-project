# Kullanım

Uygulama docker veya VS 2019 ile başlatılabilir.

Uygulama src/EcommerceSample.Console/data.txt içerisindeki komutları kullanmaktadır. Komutlar değiştirilebilir ya da yeni komutlar eklenebilir.

Docker ile çalıştırmak için;

Ana dizin içerisinde aşağıdaki komutları sıralı olarak çalıştırabilirsiniz. Komutlar Windows OS'ler içindir. Linux'da komutlar değişiklik gösterebilir.

```shell
docker build -f src\EcommerceSample.Console\Dockerfile -t console-app .
```

```shell
docker run console-app
```


# Uygulama Detayları
Uygulama .Net Core 2.2 ile kodlanmıştır. Database olarak in memory database kullanmıştır. EF Core 2.2 ile InMemoryDb kullanımını Console uygulamada yapabilmek için AspNetCore'un bazı paketleri kullanılmıştır.

Console.App içerisinde SimpleInjector kullanılmıştır. Console uygulama olduğu için bazı type'lar Singleton/thread lifestyle kullanılmıştır. Uygulamada bu tip tanımları gözardı ederek bakınız.