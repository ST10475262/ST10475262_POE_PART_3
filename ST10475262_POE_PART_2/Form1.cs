using System.Media;

namespace ST10475262_POE_PART_2
{
    public partial class Form1 : Form
    {
        TasksDatabase db = new TasksDatabase();
        //remembering the tasks added
        bool waitingForReminder = false;
        int lastTaskId = -1;
        

        List<ResponseDelegate> responseHandlers = new List<ResponseDelegate>(); 

        System.Windows.Forms.Timer typingTimer = new System.Windows.Forms.Timer();
        string fullTypingText = "";   
        int typingIndex = 0;    
        Label typingLabel = null;
        bool isTyping = false; 


        bool waitingForName = false; 
        bool startupDone = false; 
        bool askNameAfterTyping = false; 
        bool exitAfterTyping = false; 


        public Form1()
        {
            InitializeComponent();
            SetupDelegateList();   //fill the delegate list with all topic methods
            SetupTypingTimer();    //configure the typing timer
            //PlayStartupSound();    //play  cypherr.wav
            ShowWelcomeMessage();  //display the welcome message then prompt for name
        }

        private void SetupDelegateList()
        {
            responseHandlers.Add(RobotResponses.DetectSentiment);

            // MEMORY / CONTEXT FIRST
            responseHandlers.Add(RobotResponses.RememberName);
            responseHandlers.Add(RobotResponses.RememberInterest);

            // NORMAL TOPICS AFTER
            responseHandlers.Add(RobotResponses.Hello);
            responseHandlers.Add(RobotResponses.Greeting);
            responseHandlers.Add(RobotResponses.Purpose);
            responseHandlers.Add(RobotResponses.Help);
            responseHandlers.Add(RobotResponses.PasswordManagers);
            responseHandlers.Add(RobotResponses.Passwords);
            responseHandlers.Add(RobotResponses.TwoFactorAuthentication);
            responseHandlers.Add(RobotResponses.Phishing);
            responseHandlers.Add(RobotResponses.Malware);
            responseHandlers.Add(RobotResponses.Antivirus);
            responseHandlers.Add(RobotResponses.SocialEngineering);
            responseHandlers.Add(RobotResponses.DataPrivacy);
            responseHandlers.Add(RobotResponses.SafeBrowsing);
            responseHandlers.Add(RobotResponses.TellMeMore);
            responseHandlers.Add(RobotResponses.Exit);
        }

        private void ShowWelcomeMessage() //types the welcome message, then queues the name prompt
        {
            askNameAfterTyping = true; //once the welcome message finishes typing, automatically call AskForName()
            AddBotMessage("Welcome to Cypherr Bot - your Cybersecurity Awareness Assistant!"); //type the welcome message
        }

        private void SetupTypingTimer() //configures the timer used for the typing effect
        {
            typingTimer.Interval = 18;
            typingTimer.Tick += new EventHandler(TypingTimer_Tick);
        }

        private void TypingTimer_Tick(object sender, EventArgs e) 
        {
            if (typingIndex < fullTypingText.Length) //still characters left to reveal
            {
                typingLabel.Text = fullTypingText.Substring(0, typingIndex + 1); //reveal one more character into the label
                typingIndex++;                                                    //move to the next character position
                ScrollToBottom();
            }
            else //all characters have been revealed
            {
                typingTimer.Stop(); 
                isTyping = false;   //mark that typing has finished

                if (exitAfterTyping)
                {
                    btnSend.Enabled = false;
                    txtInput.Enabled = false;
                    return;
                }

                if (askNameAfterTyping) 
                {
                    askNameAfterTyping = false; 
                    AskForName();
                    return;
                }

               
                btnSend.Enabled = true;
                txtInput.Enabled = true;
                txtInput.Focus(); 
            }
        }

        private void StartTypingEffect(Label label, string fullText) 
        {
            typingLabel = label;    
            fullTypingText = fullText; 
            typingIndex = 0; 
            isTyping = true;   

            btnSend.Enabled = false;  
            txtInput.Enabled = false;  
            typingTimer.Start();       
        }

        private void AddBotMessage(string message) //adds a bot message to the left side and starts typing it out
        {
            Label nameLabel = new Label();
            nameLabel.Text = "Cypherr";
            nameLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            nameLabel.ForeColor = Color.FromArgb(56, 189, 248); //cyan colour for the bot name
            nameLabel.AutoSize = true;
            nameLabel.Margin = new Padding(4, 6, 4, 0); //a little space above each message

            Label messageLabel = new Label();
            messageLabel.Text = "";
            messageLabel.Font = new Font("Segoe UI", 10f);
            messageLabel.ForeColor = Color.FromArgb(56, 189, 248); //cyan text for bot messages
            messageLabel.AutoSize = true;
            messageLabel.MaximumSize = new Size(panelDisplay.ClientSize.Width - 40, 0); //limit width to roughly half the panel
            messageLabel.Margin = new Padding(4, 0, 4, 4);

            panelDisplay.Controls.Add(nameLabel);    
            panelDisplay.Controls.Add(messageLabel); 
            ScrollToBottom();

            StartTypingEffect(messageLabel, message); //start typing the response into this label character by character
        }

        private void AskForName() //asks the user to enter their name
        {
            waitingForName = true;  
            startupDone = false; 

            txtInput.Enabled = true; 
            btnSend.Enabled = true;
            txtInput.Focus();

            AddBotMessage("Before we begin, what is your name?"); 
        }


        private void AddUserMessage(string message) 
        {
            string displayName = "User";
            if (RobotResponses.memory.ContainsKey("name"))
            {
                displayName = RobotResponses.memory["name"]; 
            }

            Label nameLabel = new Label();
            nameLabel.Text = displayName;
            nameLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            nameLabel.ForeColor = Color.FromArgb(74, 222, 128); //green colour for the user name
            nameLabel.AutoSize = true;
            nameLabel.Margin = new Padding(4, 6, 4, 0);

            Label messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.Font = new Font("Segoe UI", 10f);
            messageLabel.ForeColor = Color.FromArgb(74, 222, 128); //green text for user messages
            messageLabel.AutoSize = true;
            messageLabel.MaximumSize = new Size(panelDisplay.ClientSize.Width - 40, 0);
            messageLabel.Margin = new Padding(4, 0, 4, 4);

            panelDisplay.Controls.Add(nameLabel);    
            panelDisplay.Controls.Add(messageLabel);
            ScrollToBottom();
        }


        private void ScrollToBottom() //scrolls the chat to show the most recent message
        {
            if (panelDisplay.Controls.Count > 0)
            {
                panelDisplay.ScrollControlIntoView(panelDisplay.Controls[panelDisplay.Controls.Count - 1]); //scroll to the last control
            }
        }

        private void PlayStartupSound() //method to play the startup sound from part 1
        {
            try
            {
                if (OperatingSystem.IsWindows())
                {
                    string wavPath = @"C:\Users\HPP\source\repos\ST10475262_POE_PART_2\ST10475262_POE_PART_2\cypherr.wav";

                    if (System.IO.File.Exists(wavPath))
                    {
                        SoundPlayer greeting = new SoundPlayer(wavPath);
                        greeting.Load();
                        greeting.Play();
                    }
                }
            }
            catch
            {
                //continue without sound if it fails to play
            }
        }

        private void HandleReminder(string input)
        {
            input = input.ToLower();

            DateTime reminderDate;

            if (DateTime.TryParse(input, out reminderDate))
            {
            }
            else if (input.Contains("tomorrow"))
            {
                reminderDate = DateTime.Today.AddDays(1);
            }
            else if (input.Contains("3 day"))
            {
                reminderDate = DateTime.Today.AddDays(3);
            }
            else if (input.Contains("7 day"))
            {
                reminderDate = DateTime.Today.AddDays(7);
            }
            else
            {
                waitingForReminder = false;

                AddBotMessage("No reminder was set.");

                return;
            }

            db.UpdateReminder(lastTaskId, reminderDate);
            ActivityLogger.Add($"Reminder set for Task #{lastTaskId} on {reminderDate:dd MMM yyyy}");
            waitingForReminder = false;

            AddBotMessage($"Reminder set for {reminderDate:dd MMM yyyy}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userInput = txtInput.Text.Trim();

            if (waitingForName)
            {
                AddUserMessage(userInput);
                txtInput.Clear();

                RobotResponses.RememberName(userInput);

                waitingForName = false;
                startupDone = true;

                AddBotMessage($"Hi { userInput}, I can help you learn about cybersecurity.Try asking 'what can you do' or 'help'.");


                return;
            }

            // Empty message
            if (userInput == "")
            {
                return;
            }

            // Display user's message
            AddUserMessage(userInput);
            txtInput.Clear();

            if (waitingForReminder)
            {
                AddUserMessage(userInput);
                txtInput.Clear();

                HandleReminder(userInput);

                return;
            }

            string botResponse = "";
            string lowercaseInput = userInput.ToLower(); 
            string sentiment = RobotResponses.DetectSentiment(lowercaseInput);

            foreach (ResponseDelegate handler in responseHandlers)
            {
                if (handler == RobotResponses.DetectSentiment)
                    continue;

                string result = handler(lowercaseInput);

                if (result != "")
                {
                    botResponse = result;
                    break;
                }
            }

            if (botResponse == "")
            {
                botResponse = RobotResponses.DefaultResponse();
            }

            if (sentiment != "")
            {
                botResponse = sentiment + botResponse;
            }

            if (botResponse == "")
            {
                botResponse = RobotResponses.DefaultResponse(); //call the default response
            }

            if (HandleTaskCommands(userInput))
            {
                return;
            }

            if (botResponse.StartsWith("EXIT|"))
            {
                string goodbyeMessage = botResponse.Replace("EXIT|", ""); //remove the EXIT| prefix
                AddBotMessage(goodbyeMessage); 
                txtInput.Enabled = false;   
                btnSend.Enabled = false;    
            }
            else
            {
                AddBotMessage(botResponse); 
            }
        }

        private void panelDisplay_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInput_Enter(object sender, EventArgs e)
        {

        }

        private bool HandleTaskCommands(string userInput)
        {
            //add tasks
            if (userInput.ToLower().StartsWith("add task"))
            {
                string taskData = userInput.Substring(8).Trim();

                string[] parts = taskData.Split(':');

                if (parts.Length < 2)
                {
                    AddBotMessage("Please use:\n\n" +
                                  "add task Title : Description");

                    return true;
                }

                TaskItem task = new TaskItem();

                task.Title = parts[0].Trim();

                task.Description = parts[1].Trim();

                task.IsCompleted = false;

                task.ReminderDate = null;

                lastTaskId = db.AddTask(task);
                ActivityLogger.Add($"Task added: {task.Title}");

                waitingForReminder = true;

                AddBotMessage("Task added successfully.\n\nWould you like a reminder?");

                return true;
            }

            //complete tasks
            if (userInput.ToLower().StartsWith("complete task"))
            {
                string idText = userInput.Replace("complete task", "").Trim();

                if (int.TryParse(idText, out int id))
                {
                    db.CompleteTask(id);

                    ActivityLogger.Add($"Completed Task #{id}");

                    AddBotMessage($"Task {id} marked as completed.");
                }
                else
                {
                    AddBotMessage("Please enter a valid task number.");
                }

                return true;
            }

            //delete tasks
            if (userInput.ToLower().StartsWith("delete task"))
            {
                string idText = userInput.Replace("complete task", "").Trim();

                if (int.TryParse(idText, out int id))
                {
                    db.CompleteTask(id);

                    ActivityLogger.Add($"Completed Task #{id}");

                    AddBotMessage($"Task {id} marked as completed.");
                }
                else
                {
                    AddBotMessage("Please enter a valid task number.");
                }

                return true;
            }

            //show logs
            if (userInput.ToLower() == "show activity log" || userInput.ToLower() == "what have you done for me?")
            {
                List<string> logs = ActivityLogger.GetRecent();

                if (logs.Count == 0)
                {
                    AddBotMessage("No activity has been recorded yet.");

                    return true;
                }

                string output ="Here's a summary of recent actions:\n\n";

                foreach (string log in logs)
                {
                    output += "- " + log + "\n";
                }

                AddBotMessage(output);

                return true;
            }
            return false;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //if the key pressed was Enter
            {
                e.SuppressKeyPress = true; 
                button1_Click(sender, e); 
            }
        }
    }
}