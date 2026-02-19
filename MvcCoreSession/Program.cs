using MvcCoreSession.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//REALIZAMOS ESTA INYECCIÓN PARA EL CONTEXT ANCESSOR
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//e INYECTAMOS LA CLASE QUE HEMOS CREADO
builder.Services.AddSingleton<HelperSessionContextAccessor>();

//MEMORIA DISTRUBUIDA ARA QUE SESSION FUNCIONE
builder.Services.AddDistributedMemoryCache();
//CREAMOS UN SERVICIO DE SESSION,con tiempo
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

//HABILITAMOS SESSION PARA EL SERVER()EL ORDEN IMPORTA,POR ESO LO PONEMOS AQUÍ
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
