using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Reflection;

namespace TrainingManagement.Data
{
    public static class SqlSugarConfig
    {
        public static void ConfigureSqlSugar(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddScoped<ISqlSugarClient>(provider => 
            {
                var db = new SqlSugarScope(new ConnectionConfig()
                {
                    ConnectionString = connectionString,
                    DbType = DbType.MySql,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute,
                    //根据字段？设置数据库是否可空
                    ConfigureExternalServices=new ConfigureExternalServices()
                    {
                        EntityService = (c, p) =>
                        {
                            if (p.IsPrimarykey == false && new NullabilityInfoContext()
                                .Create(c).WriteState is NullabilityState.Nullable)
                            {
                                p.IsNullable = true;
                            }
                        }
                    }
                },
                db => {
                    // 配置AOP
                    db.Aop.OnLogExecuting = (sql, pars) => 
                    {
                        Console.WriteLine(sql);
                    };

                });
                
                return db;
            });
        }
    }
}