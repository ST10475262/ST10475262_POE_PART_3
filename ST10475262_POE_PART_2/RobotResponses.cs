using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ST10475262_POE_PART_2
{

    public delegate string ResponseDelegate(string input); //delegate that represents any method which takes a string input and returns a string response


    public class RobotResponses
    {
        static Random rand = new Random();//global randomizer for responses
        static string botName = "Cypherr"; //global variable to store the bot's name

        public static Dictionary<string, string> memory = new Dictionary<string, string>(); //stores things the bot remembers about the user
        public static string lastTopic = ""; //used for conversational flow


        public static string Hello(string input) //method to handle user greetings
        {
            if (input.Contains("hi") || input.Contains("hello") || input.Contains("hey"))
            {
                //check if we already remember the user's name to personalise the greeting
                string name = "";
                if (memory.ContainsKey("name")) //if the bot has already stored the user's name
                {
                    name = " " + memory["name"]; //retrieve their name from memory and add it
                }

                //an array to store responses to questions related to greetings
                string[] responses = { "hi" + name + "! Hope you're doing great! Ready to talk cybersecurity?",
                                       "hey" + name + "! Let's talk cybersecurity!!",
                                       "hello" + name + "! Hope you're doing great! Ready to talk about cybersecurity?" };

                lastTopic = "greeting"; //update lastTopic

                return " " + responses[rand.Next(responses.Length)]; //selects a random response from the array
            }
            return ""; //return an empty string to signal that this method did not match - the main form checks for empty string
        }

        public static string Greeting(string input)
        {
            if (input.Contains("how are you") || input.Contains("how's it going") || input.Contains("how are you doing"))
            {
                

               

                //an array to store responses to questions related to greetings
                string[] responses ={"I'm running smoothly! Ready to discuss cybersecurity?",
                                     "All systems operational! Let's talk cybersecurity!!",
                                     "Doing great! Ready to talk about cybersecurity?",
                                     "I'm good! How can I help you stay safe online?"};

                lastTopic = "greeting"; //update lastTopic

                return " " + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string Purpose(string input)//method to state the purpose / use of the bot
        {
            if (input.Contains("purpose") || input.Contains("what do you do") || input.Contains("what can you do"))
            {
                
                //the array storing responses to the specified keywords
                string[] responses ={"My purpose is to help people learn about cybersecurity.",
                                     "I provide tips on staying safe online.",
                                     "I'm here to answer questions about internet safety."};

                

                
                return " " + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string Help(string input)// method to give the user topics to question the bot about
        {
            if (input.Contains("what can i ask") || input.Contains("help with") || input.Contains("help"))
            {
                
                
                return " " + "You can ask me about passwords, password managers, 2FA(Two - Factor Authentication),\n" +
                                  "phishing, malware, antiviruses, social engrinnering, data privacy and safe browsing.";
            }
            return "";
        }

        public static string Passwords(string input)
        {
            

            if (input.Contains("password") || input.Contains("password safety"))//method to respond to password safety
            {

                lastTopic = "password"; //update lastTopic so conversational flow can reference this topic later

                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"]=="password")
                {
                    prefix = "As someone interested in passwords, "; //use recalled memory to personalise the response
                }
                if (memory.ContainsKey("name")) //also add the user's name if we remember it
                {
                    prefix = memory["name"] + ", " + prefix;
                }

                string[] responses = {"When creating a password, ALWAYS avoid using personal information such as your name, birthdate,\n" +
                                      "etc. Use a combination of uppercase and lowercase letters, numbers, and special characters to \n" +
                                      "make your password stronger.\n\n" +
                                      "It is also important to use a different password for each account. Consider using a\n" +
                                      "password manager and enable two-factor authentication for added security.",

                                      "A strong password is essential for protecting your accounts from hackers. Avoid using common\n" +
                                      "words and predictable patterns. Instead, ypu must create long and unique passwords that are hard\n" +
                                      "to guess.\n\n" +
                                      "Never reuse passwords across multiple platforms. If one gets compromised, all your\n" +
                                      "accounts could be at risk. Using a password manager can help keep track of them safely\n" +
                                      "and two-factor authentication increases the security on you account.",

                                      "Cybercriminals often target weak passwords using automated attacks. Avoid using any words related\n" +
                                      "to you such as names, dates and hobbies. To ensure that you stay safe and protect your account, make\n" +
                                      "sure your password is at least 12 characters long and includes a variety of character types.\n\n" +
                                      "Enable two-factor authentication (2FA) wherever possible, as it adds an extra layer of\n" +
                                      "protection beyond just your password."};




                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string PasswordManagers(string input)//method to respond to password managers
        {
            if (input.Contains("password manager"))
            {
                lastTopic = "password manager"; //update lastTopic

                //check memory to personalise the response if the user said they were interested in privacy
                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"]=="password manager")
                {
                    prefix = "As someone interested in password managers, "; //use recalled memory to personalise the response
                }

                string[] responses =  {"A password manager securely stores all your passwords in one place.\n" +
                                       "You only need to remember one master password to access everything.\n\n" +
                                       "It can also generate strong, unique passwords for each account,\n" +
                                       "helping you avoid reuse and improving your security.",

                                       "Managing multiple passwords can be difficult, which is why password managers are useful.\n" +
                                       "They store your login details securely and autofill them when needed.\n\n" +
                                       "Most password managers use encryption, meaning your data is protected\n" +
                                       "even if someone gains access to the storage.",

                                       "Using a password manager reduces the risk of weak or repeated passwords.\n" +
                                       "It helps you create long, complex passwords without needing to remember them.\n\n" +
                                       "This is one of the easiest ways to improve your overall cybersecurity."};




                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string TwoFactorAuthentication(string input)//method to respond to two-factor authentication
        {
            if (input.Contains("2fa") || input.Contains("two factor") || input.Contains("two-factor"))
            {
                lastTopic = "2fa"; //update lastTopic

                //check memory to personalise the response if the user said they were interested in 2fa
                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"]=="2fa")
                {
                    prefix = "As someone interested in 2FA, "; 
                }


                string[] responses ={"Two-Factor Authentication (2FA) adds an extra layer of security to your accounts.\n" +
                                     "In addition to your password, you must provide a second form of verification.\n\n" +
                                     "This could be a code sent to your phone or generated by an authenticator app.",

                                     "Even if someone steals your password, 2FA can stop them from accessing your account.\n" +
                                     "It requires a second step, such as a fingerprint, OTP, or security key.\n\n" +
                                     "This makes your accounts much harder to compromise.",

                                     "Enabling 2FA is one of the best ways to protect sensitive accounts.\n" +
                                     "It ensures that logging in requires something you know (password)\n" +
                                     "and something you have (like your phone).\n\n" +
                                     "Always enable it on email, banking, and social media accounts."};




                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string Phishing(string input)//method to respond to phishing
        {
            if (input.Contains("phishing"))
            {
                lastTopic = "phishing"; //update lastTopic

                //check memory to personalise the response if the user said they were interested in phishing
                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"]=="phishing")
                {
                    prefix = "As someone interested in phishing, "; //use recalled memory to personalise the response
                }


                string[] responses ={"Phishing is a cyber attack where attackers trick you into revealing sensitive information.\n" +
                                     "They often pretend to be trusted organisations through emails or messages.\n\n" +
                                     "Always verify the sender before clicking any links or sharing personal data.",

                                     "Attackers use phishing to steal passwords, banking details, and personal information.\n" +
                                     "They create fake websites or urgent messages to pressure you into acting quickly.\n\n" +
                                     "Be cautious of unexpected emails and avoid clicking suspicious links.",

                                     "A common sign of phishing is urgency, such as messages claiming your account is at risk.\n" +
                                     "These messages try to make you panic and act without thinking.\n\n" +
                                     "Always double-check URLs and confirm requests directly with the organisation."};




                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string Malware(string input)//method to respond to malware
        {
            if (input.Contains("malware") || input.Contains("virus"))
            {
                lastTopic = "malware"; //update lastTopic

                //check memory to personalise the response if the user said they were interested in malware
                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"]=="malware")
                {
                    prefix = "As someone interested in malware, "; //use recalled memory to personalise the response
                }



                string[] responses ={"Malware is malicious software designed to harm your device or steal your data.\n\n" +
                                     "It includes viruses, worms, trojans, ransomware, spyware, and adware.\n" +
                                     "Each type behaves differently but all aim to compromise your system.\n\n" +
                                     "Avoid downloading files from untrusted sources and always keep your system updated.",

                                     "Viruses and malware can spread through infected files, downloads, or email attachments.\n" +
                                     "Some malware runs silently in the background, collecting your personal information.\n\n" +
                                     "Be cautious of unknown links and always scan files before opening them.\n" +
                                     "Using security software greatly reduces your risk.",

                                     "Ransomware is a dangerous type of malware that locks your files and demands payment.\n" +
                                     "Spyware secretly tracks your activity, while trojans disguise themselves as safe programs.\n\n" +
                                     "Keeping backups of your data and avoiding suspicious downloads can protect you."};




                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string Antivirus(string input)//method to respond to antiviruses
        {
            if (input.Contains("antivirus"))
            {
                lastTopic = "antivirus"; //update lastTopic

                //check memory to personalise the response if the user said they were interested in antivirus
                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"]=="antivirus")
                {
                    prefix = "As someone interested in antiviruses, "; //use recalled memory to personalise the response
                }



                string[] responses ={"Antivirus software helps detect and remove malware from your device.\n\n" +
                                     "It scans files and programs for suspicious behavior and blocks threats in real time.\n" +
                                     "Keeping it updated ensures protection against the latest threats.",

                                     "A good antivirus program acts as your first line of defense against cyber attacks.\n\n" +
                                     "It can quarantine infected files and prevent harmful software from running.\n" +
                                     "Regular scans help keep your system clean and secure.",

                                     "Antivirus tools use databases of known threats and behavior analysis to detect malware.\n\n" +
                                     "Enabling real-time protection and scheduling automatic scans\n" +
                                     "can significantly reduce your chances of infection."};




                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string SocialEngineering(string input)//method to respond to social engineering
        {
            if (input.Contains("social engineering"))
            {
                lastTopic = "social engineering"; //update lastTopic

                //check memory to personalise the response if the user said they were interested in social engineering
                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"]=="social engineering")
                {
                    prefix = "As someone interested in social engineering, "; //use recalled memory to personalise the response
                }


                string[] responses ={"Social engineering attacks manipulate people into revealing sensitive information.\n\n" +
                                     "Attackers often pretend to be trusted individuals or organisations.\n" +
                                     "This can include fake emails, phone calls, or messages.\n\n" +
                                     "Always verify identities before sharing personal information.",

                                     "Instead of hacking systems, social engineering targets human behavior.\n\n" +
                                     "Common tactics include phishing, impersonation, and urgent requests.\n" +
                                     "Attackers rely on fear and urgency to trick victims.\n\n" +
                                     "Stay calm and double-check suspicious requests.",

                                     "A common social engineering trick is pretending to be technical support or a bank.\n\n" +
                                     "They may ask for passwords or sensitive details.\n" +
                                     "Legitimate organisations will never ask for this information directly.\n\n" +
                                     "Never share confidential data with unverified sources."};




                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string DataPrivacy(string input)//method to respond to data privacy
        {
            if (input.Contains("data privacy") || input.Contains("privacy"))
            {
                lastTopic = "data privacy"; //update lastTopic

                //check memory to personalise the response if the user said they were interested in data privacy
                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"]=="data privacy")
                {
                    prefix = "As someone interested in data privacy, "; //use recalled memory to personalise the response
                }


                string[] responses ={"Data privacy is about protecting your personal information online.\n\n" +
                                     "This includes your name, location, financial details, and browsing activity.\n" +
                                     "Limiting what you share online helps reduce your risk.",

                                     "Many websites collect user data, sometimes without you realising it.\n\n" +
                                     "Always review privacy settings and permissions on apps and platforms.\n" +
                                     "Only share information that is necessary.",

                                     "Protecting your data involves using strong passwords and secure connections.\n\n" +
                                     "Avoid public Wi-Fi for sensitive transactions and use trusted applications.\n" +
                                     "Being cautious online helps keep your information safe."};




                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }


        public static string SafeBrowsing(string input)
        {
            if (input.Contains("safe browsing") || input.Contains("browse safely")) //method to respond to safely browsing the internet
            {
                lastTopic = "safe browsing"; //update lastTopic

                //check memory to personalise the response if the user said they were interested in safe browsing
                string prefix = "";
                if (memory.ContainsKey("interest") && memory["interest"] == "safe browsing")
                {
                    prefix = "As someone interested in safe browsing, "; //use recalled memory to personalise the response
                }



                string[] responses ={"Safe browsing means using the internet in a way that protects your data and devices.\n\n" +
                                     "Always check for HTTPS in website URLs to ensure a secure connection.\n" +
                                     "Avoid clicking suspicious links or pop-ups.",
                                      
                                     "Many cyber threats come from unsafe websites and downloads.\n\n" +
                                     "Only download files from trusted sources and avoid unknown links.\n" +
                                     "Keeping your browser updated helps protect against vulnerabilities.",

                                     "Be cautious when browsing unfamiliar websites.\n\n" +
                                     "Look out for signs like poor design, strange URLs, or unexpected downloads.\n" +
                                     "Using security tools and updated browsers improves your safety online."};

                

                
                return " " + prefix + responses[rand.Next(responses.Length)];
            }
            return "";
        }

        public static string RememberName(string input) //method to remember the user's name
        {
            if (input.Contains("my name is") || input.Contains("call me"))
            {
                string name = ""; //will hold the name we extract from the input

                //extract the name by removing the phrase and keeping what's left
                if (input.Contains("my name is"))
                {
                    name = input.Replace("my name is", "").Trim(); //remove "my name is" and trim spaces
                }
                else if (input.Contains("call me"))
                {
                    name = input.Replace("call me", "").Trim(); //remove "call me" and trim spaces
                }

                //capitalise the first letter to make the name look proper
                if (name.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        name = char.ToUpper(name[0]) + name.Substring(1);
                    }
                    else
                    {
                        return "";
                    }
                }

                memory["name"] = name; //store the name in the memory dictionary so we can use it later

                return " Nice to meet you, " + name + "! I'll remember your name. How can I help you today?";
            }
            return ""; //return empty string if keyword not found
        }

        public static string RememberInterest(string input)
        {
            if (input.Contains("i'm interested in") ||
                input.Contains("i am interested in") ||
                input.Contains("interested in"))
            {
                string topic = "";

                // extract topic
                if (input.Contains("i'm interested in"))
                {
                    topic = input.Replace("i'm interested in", "").Trim();
                }
                else if (input.Contains("i am interested in"))
                {
                    topic = input.Replace("i am interested in", "").Trim();
                }
                else if (input.Contains("interested in"))
                {
                    topic = input.Replace("interested in", "").Trim();
                }

                // convert to lowercase
                topic = topic.ToLower();

                // valid cybersecurity topics
                List<string> validTopics = new List<string>(){"password","passwords","password manager","password managers","2fa","two factor authentication","phishing","malware","virus","antivirus","social engineering","data privacy","privacy","safe browsing"};

                // check if topic is valid
                bool valid = false;

                foreach (string validTopic in validTopics)
                {
                    if (topic.Contains(validTopic))
                    {
                        valid = true;
                        topic = validTopic;
                        break;
                    }
                }

                // if invalid/misspelled
                if (!valid)
                {
                    return "I'm not sure what topic that is. Please ask about a cybersecurity topic like passwords, phishing, malware, 2FA, or privacy.";
                }

                // save memory
                memory["interest"] = topic;

                return "Great! I'll remember that you're interested in " + topic +
                       ". Feel free to ask me anything about it!";
            }

            return "";
        }


        public static string TellMeMore(string input) //method to handle "tell me more" and follow-up requests
        {
            if (input.Contains("tell me more") || input.Contains("more info") || input.Contains("explain more") || input.Contains("another tip") || input.Contains("give me more") || input.Contains("elaborate"))
            {
                //check the last topic and re-call the right method to give another tip
                if (lastTopic == "password")
                {
                    return Passwords("password"); //call passwords method again for a new random tip
                }
                else if (lastTopic == "password manager")
                {
                    return PasswordManagers("password manager"); //call password managers method again
                }
                else if (lastTopic == "2fa")
                {
                    return TwoFactorAuthentication("2fa"); //call 2fa method again
                }
                else if (lastTopic == "phishing")
                {
                    return Phishing("phishing"); //call phishing method again
                }
                else if (lastTopic == "malware")
                {
                    return Malware("malware"); //call malware method again
                }
                else if (lastTopic == "antivirus")
                {
                    return Antivirus("antivirus"); //call antivirus method again
                }
                else if (lastTopic == "social engineering")
                {
                    return SocialEngineering("social engineering"); //call social engineering method again
                }
                else if (lastTopic == "data privacy")
                {
                    return DataPrivacy("privacy"); //call data privacy method again
                }
                else if (lastTopic == "safe browsing")
                {
                    return SafeBrowsing("safe browsing"); //call safe browsing method again
                }
                else
                {
                    return " Sure! What topic would you like more information on? You can ask about passwords, phishing, malware, and more."; //no last topic to go back to
                }
            }
            return ""; //return empty string if keyword not found
        }


        public static string DetectSentiment(string input) //method to detect emotions in the user's message
        {
            string sentimentResponse = ""; //will hold the empathy part of the response
            string tipResponse = "";       //will hold the automatic tip that follows the empathy

            //check for worried / scared sentiment
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid") || input.Contains("anxious"))
            {
                sentimentResponse = " It's completely understandable to feel that way. Scammers and cybercriminals can be very convincing. Let me share some tips to help you stay safe.\n\n";
            }

            //check for frustrated / confused sentiment
            if (input.Contains("frustrated") || input.Contains("confused") || input.Contains("annoyed"))
            {
                sentimentResponse = " I hear you - this stuff can be tricky! Don't worry, let's work through it together.\n\n";
            }

            //check for curious sentiment
            if (input.Contains("curious") || input.Contains("wondering"))
            {
                sentimentResponse = " Great curiosity! Asking questions is the first step to staying safe online.\n\n";
            }

            //if a sentiment was detected, check if the user also mentioned a cybersecurity topic in the same message
            if (sentimentResponse != "")
            {
                if (input.Contains("phishing") || input.Contains("scam"))
                {
                    tipResponse = Phishing("phishing"); //automatically get and attach a phishing tip
                }
                else if (input.Contains("password"))
                {
                    tipResponse = Passwords("password"); //automatically get and attach a password tip
                }
                else if (input.Contains("malware") || input.Contains("virus"))
                {
                    tipResponse = Malware("malware"); //automatically get and attach a malware tip
                }
                else if (input.Contains("privacy"))
                {
                    tipResponse = DataPrivacy("privacy"); //automatically get and attach a privacy tip
                }
                else if (input.Contains("2fa") || input.Contains("two factor"))
                {
                    tipResponse = TwoFactorAuthentication("2fa"); //automatically get and attach a 2fa tip
                }
                else
                {
                    tipResponse = " Here is a general tip: always keep your software updated, use strong passwords, and be cautious of suspicious links."; //general tip if no specific topic was found
                }

                return sentimentResponse + tipResponse; //return the empathy message AND the automatic tip together
            }

            return ""; //return empty string if no sentiment was detected in the input
        }

        public static string DefaultResponse() //method to handle inputs that don't match any known keyword
        {
            string[] responses = { "I'm not sure I understand. Try asking about cybersecurity.",
                                   "Could you rephrase that? I can help with passwords, phishing, and online safety.",
                                   "Please rephrase your question. Try asking about passwords, phishing, or malware.",
                                   "Try asking about a cybersecurity topic. Type 'help' to see what I can assist with." };

            return " " + responses[rand.Next(responses.Length)]; //selects a random default response from the array
        }


        public static string Exit(string input) //method to handle when the user wants to leave
        {
            if (input.Contains("exit") || input.Contains("quit") || input.Contains("bye") || input.Contains("goodbye"))
            {
                //check if we remember the user's name for a personalised goodbye
                string name = "";
                if (memory.ContainsKey("name"))
                {
                    name = " " + memory["name"]; //retrieve their name from memory
                }

                return "EXIT|" + " Goodbye" + name + "! Stay safe online."; //EXIT| is a prefix the form checks for to disable the input box
            }
            return ""; //return empty string if keyword not found
        }
    }
}