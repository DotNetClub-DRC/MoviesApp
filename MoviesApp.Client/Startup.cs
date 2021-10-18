namespace MoviesApp.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<IMovieService, MovieService>();

            // Accessing IdentityServer4 via config.cs file
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:6001"; // IdentityServer4 Url
                options.ClientId = "movie_mvc_client";
                options.ClientSecret = "sudidav";
                options.ResponseType = "code id_token";

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("movieAPI");

                options.SaveTokens = true;

                options.GetClaimsFromUserInfoEndpoint = true;
            });

            services.AddTransient<AuthenticationDelegatingHandler>();

            services.AddHttpClient("MovieAPIClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001"); // API baseUrl
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:6001"); // IdentityServer4 baseUrl
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            services.AddHttpContextAccessor();
            //services.AddSingleton(new ClientCredentialsTokenRequest
            //{
            //    Address = "https://localhost:6001/connect/token",
            //    ClientId= "movieClient",
            //    ClientSecret= "sudidav",
            //    Scope = "movieAPI"
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
