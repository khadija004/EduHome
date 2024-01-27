using Domain.Entities.ViewComponentModel;
using System.Collections.Generic;

namespace EduHome.ViewModels.ViewComponentViewModels
{
    public class NoticeBoardVM
    {
        public NoticeVideoVC NoticeVideo { get; set; }
        public List<NoticeVC> Notices { get; set; }
    }
}
