using System;
using System.Collections.Generic;

class Video
{
    public string Title { get; }
    public string Author { get; }
    public int LengthInSeconds { get; }
    public List<Comment> Comments { get; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string CommenterName { get; }
    public string Text { get; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create and add videos to the list
        Video video1 = new Video("Video 1", "Jose", 110);
        Video video2 = new Video("Video 2", "Peter", 150);
        Video video3 = new Video("Video 3", "Juliette", 190);

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        video1.Comments.Add(new Comment("Romel", "Great video!"));
        video1.Comments.Add(new Comment("Luz", "Interesting content."));
        video1.Comments.Add(new Comment("Christian", "I learned a lot."));

        video2.Comments.Add(new Comment("Joseph", "Nice work!"));
        video2.Comments.Add(new Comment("Hilda", "Very informative."));
        video2.Comments.Add(new Comment("Camila", "Keep it up!"));

        video3.Comments.Add(new Comment("Angie", "Short but sweet."));
        video3.Comments.Add(new Comment("Frank", "I wish it was longer."));
        video3.Comments.Add(new Comment("Rose", "Good explanation."));

        foreach (var video in videos)
        {
            Console.WriteLine($"Video Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length (seconds): {video.LengthInSeconds}");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");
            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}
