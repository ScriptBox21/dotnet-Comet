﻿using System;
using System.Collections.Generic;
using HotUI.Samples.Comparisons;

namespace HotUI.Samples {
    public class MenuItem
    {
        public MenuItem()
        {

        }
        public MenuItem(string title, Func<View> page)
        {
            Title = title;
            Page = page;
        }
        public string Title { get; set; }
        public Func<View> Page { get; set; }
    }

	public class MainPage : View {
		List<MenuItem> pages = new List<MenuItem> {
			new MenuItem("Binding Sample!",()=> new BindingSample()),
            new MenuItem("BasicTestView",()=> new BasicTestView()),
            new MenuItem("ListPage1", ()=> new ListPage()),
            new MenuItem("ListPage2", ()=> new ListPage2()),
            new MenuItem("Insane Diff", ()=> new InsaneDiffPage()),
            new MenuItem("SwiftUI Tutorial Section 1", ()=> new Section1()),
            new MenuItem("SwiftUI Tutorial Section 2", ()=> new Section2()),
            new MenuItem("SwiftUI Tutorial Section 3", ()=> new Section3()),
            new MenuItem("SwiftUI Tutorial Section 4", ()=> new Section4()),
            new MenuItem("SwiftUI Tutorial Section 4b", ()=> new Section4b()),
            new MenuItem("SwiftUI Tutorial Section 4c", ()=> new Section4c()),
        };
		public MainPage ()
		{
			Body = () => new NavigationView {
				new ListView<MenuItem> (pages) {
					Cell = (page) => new NavigationButton (page.Title,page.Page),
				}
			};
		}

	}
}