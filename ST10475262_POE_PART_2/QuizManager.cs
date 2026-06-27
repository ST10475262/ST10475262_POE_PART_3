using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ST10475262_POE_PART_2
{
    public class QuizManager
    {
        private List<QuizQuestion> questions = new List<QuizQuestion>();

        private int currentQuestion = 0;
        private int score = 0;
        private bool quizRunning = false;
        public bool IsQuizRunning
        {
            get { return quizRunning; }
        }
        public QuizManager()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions.Add(new QuizQuestion(){
                Question = "What should you do if you receive an email asking for your password?", 
                Options = new string[]{ "A. Reply with your password\n",
                                        "B. Delete the email\n",
                                        "C. Report it as phishing\n",
                                        "D. Forward it to your friends"},
                CorrectAnswer = 'C'});

            questions.Add(new QuizQuestion()
            {
                Question = "Using the same password for every account is a good security practice.",
                Options = new string[]{ "A. True\n",
                                        "B. False"},
                CorrectAnswer = 'B'
            });
            

            questions.Add(new QuizQuestion()
            {
                Question = "What does 2FA stand for?",
                Options = new string[]{ "A.Two-Factor Authentication\n",
                                        "B.Two File Access\n",
                                        "C.Two Firewall Accounts\n",
                                        "D.Two Fast Applications"},
                CorrectAnswer = 'A'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "Which password is the strongest?",
                Options = new string[]{ "A. Password123\n",
                                        "B. 12345678\n",
                                        "C. MyDog\n",
                                        "D. P@55w0rd!8Xq#"},
                CorrectAnswer = 'D'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "You should install software updates as soon as possible.",
                Options = new string[]{ "A. True\n",
                                        "B. False"},
                CorrectAnswer = 'A'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "A website that starts with HTTPS is generally more secure than one using HTTP.",
                Options = new string[]{ "A. True\n",
                                        "B. False"},
                CorrectAnswer = 'A'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "Social engineering attacks don't rely on manipulating people rather than exploiting software.",
                Options = new string[]{ "A. True\n",
                                        "B. False"},
                CorrectAnswer = 'B'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "Which of these is the best way to protect your online accounts?",
                Options = new string[]{ "A. Share passwords with friends\n",
                                        "B. Use strong unique passwords and enable 2FA\n",
                                        "C. Write your password on your monitor\n",
                                        "D. Never log out of websites"},
                CorrectAnswer = 'B'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "What should you do before clicking a linak in an email?",
                Options = new string[]{ "A. Click it immediately\n",
                                        "B. Check who sent the email and inspect the link\n",
                                        "C. Forward it to everyone\n",
                                        "D. Forward it to your friends"},
                CorrectAnswer = 'B'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "What is phishing designed to do?",
                Options = new string[]{ "A. Improve internet speed\n",
                                        "B. Delete your emails\n",
                                        "C. Trick users into revealing sensitive information\n",
                                        "D. Protect your computer"},
                CorrectAnswer = 'C'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "WWhat is the safest way to connect to public Wi-Fi?",
                Options = new string[]{ "A. Disable your antivirus\n",
                                        "B. Share your passwords\n",
                                        "C. Turn off your firewall\n",
                                        "D. Use a VPN"},
                CorrectAnswer = 'D'
            });

            questions.Add(new QuizQuestion()
            {
                Question = "Which of the following is an example of malware?",
                Options = new string[]{ "A. Virus\n",
                                        "B. Firewall\n",
                                        "C. Password Manager\n",
                                        "D. Web Browser"},
                CorrectAnswer = 'A'
            });

        }

        public string StartQuiz()
        {
            currentQuestion = 0;
            score = 0;
            quizRunning = true;

            ActivityLogger.Add("Started Cybersecurity Quiz");

            return DisplayQuestion();
        }

        private string DisplayQuestion()
        {
            QuizQuestion q = questions[currentQuestion];

            string output = $"Question {currentQuestion + 1} of {questions}\n\n";

            output += q.Question + "\n\n";

            foreach (string option in q.Options)
            {
                output += option + "\n";
            }

            output += "\nType A, B, C or D";

            return output;
        }

        public string ProcessAnswer(string answer)
        {
            answer = answer.Trim().ToUpper();

            if (answer.Length == 0) return "Please enter A, B, C or D.";

            char userAnswer = answer[0];

            QuizQuestion q = questions[currentQuestion];

            string response = "";

            if (userAnswer == q.CorrectAnswer)
            {
                score++;

                response += "Correct!\n";
            }
            else
            {
                response += $"Incorrect.\nCorrect answer: {q.CorrectAnswer}\n";
            }

            currentQuestion++;

            if (currentQuestion >= questions.Count)
            {
                quizRunning = false;

                ActivityLogger.Add($"Completed Quiz ({score}/{questions.Count})");

                response += $"Quiz Complete!\n\n";

                response += $"Final Score: {score}/{questions.Count}\n\n";

                if (score == 12)
                    response += "Excellent! You're a cybersecurity expert!";
                else if (score >= 7)
                    response += "Great job! You know your cybersecurity.";
                else if (score >= 5)
                    response += "Good effort! Keep practising.";
                else
                    response += "Keep learning to stay safe online.";

                return response;
            }

            response += DisplayQuestion();

            return response;
        }
    }
}
