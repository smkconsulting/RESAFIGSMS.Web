using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
	options.Conventions.AuthorizePage("/Index");
}
	);
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie().AddOpenIdConnect(options =>
{
	options.ResponseType = builder.Configuration["Cognito:ResponseType"];
	options.MetadataAddress = builder.Configuration["Cognito:MetadataAddress"];
	options.ClientId = builder.Configuration["Cognito:ClientId"];
	options.Events = new OpenIdConnectEvents
	{
		OnRedirectToIdentityProviderForSignOut = OnRedirectToIdentityProviderForSignOut
	};
});

Task OnRedirectToIdentityProviderForSignOut(RedirectContext context)
{
	var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
	context.ProtocolMessage.Scope = "openid";
	context.ProtocolMessage.ResponseType = "code";

	var cognitoDomain = configuration["Cognito:Domain"];
	var clientId = configuration["Cognito:ClientId"];
	var appSignOutUrl = configuration["Cognito:AppSignOutUrl"];

	var logoutUrl = $"{context.Request.Scheme}://{context.Request.Host}{appSignOutUrl}";

	context.ProtocolMessage.IssuerAddress = $"{cognitoDomain}/logout?client_id={clientId}" + $"&logout_uri={logoutUrl}" + $"&redirect_uri={logoutUrl}";

	//delete cookies
	context.Properties.Items.Remove(CookieAuthenticationDefaults.AuthenticationScheme);

	//close openid session
	context.Properties.Items.Remove(OpenIdConnectDefaults.AuthenticationScheme);

	return Task.CompletedTask;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
