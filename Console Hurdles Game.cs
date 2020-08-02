using System;
using System.Threading;
class ByDenisRafi
{
	static readonly string[] 
    runningAnimation = new string[]
	{
		#region 
		@"       " + '\n' +
		@"       " + '\n' +
		@"  __*  " + '\n' +
		@" / /\_," + '\n' +
		@"__/\   " + '\n' +
		@"    \  ",
		@"       " + '\n' +
		@"       " + '\n' +
		@"   _*  " + '\n' +
		@"  |/|_ " + '\n' +
		@"  /\   " + '\n' +
		@" /  |  ",
		@"       " + '\n' +
		@"       " + '\n' +
		@"    *  " + '\n' +
		@"  </L  " + '\n' +
		@"   \   " + '\n' +
		@"   /|  ",
		@"       " + '\n' +
		@"       " + '\n' +
		@"   *   " + '\n' +
		@"   |_  " + '\n' +
		@"   |>  " + '\n' +
		@"  /|   ",
		@"       " + '\n' +
		@"       " + '\n' +
		@"   *   " + '\n' +
		@"  <|L  " + '\n' +
		@"   |_  " + '\n' +
		@"   |/  ",
		@"       " + '\n' +
		@"       " + '\n' +
		@"   *   " + '\n' +
		@"  L|L  " + '\n' +
		@"   |_  " + '\n' +
		@"  /  | ",
		@"       " + '\n' +
		@"       " + '\n' +
		@"  _*   " + '\n' +
		@" | |L  " + '\n' +
		@"   /-- " + '\n' +
		@"  /   |",
		#endregion
	};
	static readonly string[]
    jumpingAnimation = new string[]
	{
		#region 
		@"       " + '\n' +
		@"       " + '\n' +
		@"   _*  " + '\n' +
		@"  |/|_ " + '\n' +
		@"  /\   " + '\n' +
		@" /  |  ",
		@"       " + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"    *  " + '\n' +
		@"  </L  " + '\n' +
		@"   /|  ",
		@"       " + '\n' +
		@"    /*/" + '\n' +
		@"    /  " + '\n' +
		@"   //  " + '\n' +
		@"  //   " + '\n' +
		@"       ",
		@"  __*__" + '\n' +
		@" /     " + '\n' +
		@"//     " + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"       ",
		@"  __   " + '\n' +
		@" // \* " + '\n' +
		@"     \\" + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"       ",
		@"  __   " + '\n' +
		@" //_*\ " + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"       ",
		@"  __\  " + '\n' +
		@" _*/   " + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"       ",
		@" \*\__ " + '\n' +
		@"     \\" + '\n' +
		@"       " + '\n' +
		@"       " + '\n' +
		@"       ",
		@"       " + '\n' +
		@"       " + '\n' +
		@"   *   " + '\n' +
		@"  L|L  " + '\n' +
		@"   |_  " + '\n' +
		@"  /  | ",
		#endregion
	};
	static readonly string hurdleFrame =
	#region
		@"  ^^^  " + '\n' +
		@" |   | " + '\n' +
		@" |   | ";
	#endregion
	static int position = 0;
	static int? runningFrame = 0;
	static int? jumpingFrame = null;
	static void Main()
	{
		Console.Title = "Console Hurdles Game";
		Console.CursorVisible = false;
		Console.WindowWidth = 125;
		Console.WindowHeight = 25;
		Console.Clear();
		while (position < int.MaxValue)
		{
			if (Console.KeyAvailable)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.Escape:
						Console.Clear();				
						return;
					case ConsoleKey.UpArrow:
						if (!jumpingFrame.HasValue)
						{
							jumpingFrame = 0;
							runningFrame = null;
						}
						break;
				}
			}
			if (position >= 100 &&
				position % 50 == 0 &&
				(!jumpingFrame.HasValue ||
				!(2 <= jumpingFrame && jumpingFrame <= 7)))
			{
				Console.Clear();
				Console.Write("Try Again. Score " + position + ".");
				return;	
			}
			string playerFrame =
			jumpingFrame.HasValue ? jumpingAnimation[jumpingFrame.Value] :
			runningAnimation[runningFrame.Value];
			Console.SetCursorPosition(4, 10);
			Render(playerFrame, true);
			RenderHurdles(true);
			if (position % 50 == 5)
			{
				Console.SetCursorPosition(0, 13);
				Render(
					@"       " + '\n' +
					@"       " + '\n' +
					@"       ", true);
			}
			if (position % 50 < 3)
			{
				Console.SetCursorPosition(4, 10);
				Render(playerFrame, false);
				RenderHurdles(false);
			}
			else
			{
				RenderHurdles(false);
				Console.SetCursorPosition(4, 10);
				Render(playerFrame, false);
			}
			runningFrame = runningFrame.HasValue
				? (runningFrame + 1) % runningAnimation.Length
				: runningFrame;
			jumpingFrame = jumpingFrame.HasValue
				? jumpingFrame + 1
				: jumpingFrame;
			if (jumpingFrame >= jumpingAnimation.Length)
			{
				jumpingFrame = null;
				runningFrame = 2;
			}
			position++;
			Thread.Sleep(TimeSpan.FromMilliseconds(80));
		}
		Console.Clear();		
	}
	static void Render(string @string, bool renderSpace)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c is '\n') Console.SetCursorPosition(x, ++y);
			else if (!(c is ' ') || renderSpace) Console.Write(c);
			else Console.SetCursorPosition
		(Console.CursorLeft + 1, Console.CursorTop);
	}
	static void RenderHurdles(bool renderSpace)
	{
		for (int i = 5; i < Console.WindowWidth - 5; i++)
		{
			if (position + i >= 100 && (position + i - 7) % 50 == 0)
			{
				Console.SetCursorPosition(i - 3, 13);
				Render(hurdleFrame, renderSpace);
			}
		}
	}
}
