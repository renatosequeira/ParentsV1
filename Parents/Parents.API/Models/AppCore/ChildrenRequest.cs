﻿namespace Parents.API.Models.AppCore
{
    public class ChildrenRequest : Domain.Children
    {
        public byte[] ImageArray { get; set; }
        public byte[] ProfileBannerArray { get; set; }
    }
}