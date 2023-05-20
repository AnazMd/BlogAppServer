using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using BlogModels.Models;
using System.Formats.Asn1;
using System.Globalization;
using BlogAPI.Data;

public static class DbInitializer
{
    public static void Initialize(BlogDbContext context)
    {
        context.Database.Migrate();

        // Seed Posts
        if (!context.Posts.Any())
        {
            using (var reader = new StreamReader("Data/post.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Post>();
                context.Posts.AddRange(records);
                context.SaveChanges();
            }
        }

        // Seed Comments
       /* if (!context.Comments.Any())
        {
            using (var reader = new StreamReader("data/comment.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // csv.Configuration.HeaderValidated = null; // Set HeaderValidated to null
                var records = csv.GetRecords<Comment>();
                context.Comments.AddRange(records);
                context.SaveChanges();
            }
        }*/


    }
}

