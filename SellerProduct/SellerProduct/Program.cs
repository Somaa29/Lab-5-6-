using SellerProduct.IServices;
using SellerProduct.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();
/*
 * AddSingleton: Tạo ra 1 đối tượng services tồn tại cho đến khi vòng đời của ứng dụng kêt thúc. Services này sẽ được dùng
 * chung cho các request. 
 * Loại đăng kí này phù hợp với các services mang tính toàn cục và không thay đổi
 * AddScoped: Mỗi Request cụ thể sẽ tạo ra 1 đối tượng services, đối tượng này được giữ nguyên trong quá trình xử lý request phù hợp cho các serivces 
 * mà phục vụ cho một loại request cụ thể.
 * AddTransient: Mỗi Request sẽ nhận một services cụ thể khi có yêu cầu. Mỗi services sẽ được tạo mới tại thời điểm có yêu
 * cầu. Phù hợp cho các services có nhiều trạng thái, nhiều yêu cầu http và mang tính linh động hơn.
 */
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
}); // Thêm cái này để sử dụng được Session với timeOut = 10 giây
// Tất cả dịch vụ đăng kí phải trước cái dòng này OK?
var app = builder.Build(); // Thực hiện tất cả các services được cài đặt

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseStatusCodePagesWithReExecute("/Home/Index"); // tự động redirect người dùng đến /home/index khi gặp bất kì lỗi 
                                                    // nào HTTP status code
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
