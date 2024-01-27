using EduHome.ViewModels.ViewComponentViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
    public class NoticeBoardViewComponent : ViewComponent
    {
        private readonly INoticeVideoVCService _noticeVideoService;
        private readonly INoticeVCService _noticeService;

        public NoticeBoardViewComponent(INoticeVideoVCService noticeVideoService,
                                                 INoticeVCService noticeService)
        {
            _noticeVideoService = noticeVideoService;
            _noticeService = noticeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var video = await _noticeVideoService.GetVideoVC();
            var notices = await _noticeService.GetNotices();
            NoticeBoardVM noticeBoardVM = new NoticeBoardVM()
            {
                NoticeVideo = video,
                Notices = notices
            };
            return (await Task.FromResult(View(noticeBoardVM)));
        }
    }
}
