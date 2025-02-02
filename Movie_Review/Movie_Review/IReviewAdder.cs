﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Movie_Review
{
    internal interface IReviewAdder
    {
        void addReview(ref List<User> users,ref List<Reviews> reviews, ref List<Movie>movies);

        bool doesReviewExists(ref List<Reviews> reviews, string username, string moviename);
    }

    internal class ReviewAdder:IReviewAdder
    {
        public void addReview(ref List<User> users, ref List<Reviews> reviews, ref List<Movie>movies)
        {
            Console.WriteLine("Enter User Name : ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Movie Name: ");
            string moviename = Console.ReadLine();
            Console.WriteLine("Enter Rating (_/10): ");
            int rating = Convert.ToInt32(Console.ReadLine());

            

            bool youCanAddReview = false;
            bool movieExists = false;

            try
            {
                foreach (User u in users)
                {
                    if (u.username == username)
                    {
                        if (u.status == "Critic")
                        {
                            rating *= 2;
                        }
                        youCanAddReview = true;
                    }
                }

                foreach(Movie movie in movies)
                {
                    if(movie.movieName==moviename) movieExists = true;
                }

                if (youCanAddReview && movieExists)
                {
                    
                        if (!doesReviewExists(ref reviews, username, moviename))
                        {
                            Reviews review = new Reviews(username, moviename, rating);
                            reviews.Add(review);

                            Console.WriteLine();    
                            Console.WriteLine($"{username} rated movie {moviename} with {rating}/10 ratings");
                            Console.WriteLine();

                            IWrite<Reviews> writeReview=new WriteReview();
                            writeReview.Write(review);

                            Console.WriteLine("Press Any Key To Continue");
                            
                            foreach (User user in users)
                            {
                                if (user.username == username) { user.totalReviews++; user.updateUser(user); break; }
                            }
                        }
                        else
                        {
                        Console.WriteLine("\nCan not review the same movie twice.");
                        }
                }
                else
                {
                    Console.WriteLine("\nUser or movie does not exists");
                    Console.WriteLine("\nPress Any Key to Continue");
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public bool doesReviewExists(ref List<Reviews> reviews, string username, string moviename)
        {
            foreach (Reviews review in reviews) 
            {
                if (review.username == username && review.moviename == moviename) return true;
            }
            return false;
        }
    }
}
