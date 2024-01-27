using Microsoft.Extensions.DependencyInjection;
using Service.Account;
using Service.BaseModels;
using Service.Interfaces;
using Service.Setting;
using Service.Statics;
using Service.ViewComponents;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IAboutVCService, AboutVCService>();
            services.AddScoped<INoticeVCService, NoticeVCService>();
            services.AddScoped<INoticeVideoVCService, NoticeVideoVCService>();
            services.AddScoped<ITestimonialVCService, TestimonialVCService>();
            services.AddScoped<IBackgroundImagesService, BackgroundImagesService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IStaticProps, StaticProps>();

            return services;
        }
    }
}
