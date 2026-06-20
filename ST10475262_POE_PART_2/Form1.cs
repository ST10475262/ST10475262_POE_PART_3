using System.Media;

namespace ST10475262_POE_PART_2
{
    public partial class Form1 : Form
    {
        TasksDatabase db = new TasksDatabase();

        List<ResponseDelegate> responseHandlers = new List<ResponseDelegate>(); //list of delegates pointing to all our response methods

        System.Windows.Forms.Timer typingTimer = new System.Windows.Forms.Timer(); //timer that fires every 18ms
        string fullTypingText = "";   //the full response text we are currently typing out
        int typingIndex = 0;    //how many characters have been revealed so far
        Label typingLabel = null; //direct reference to the label currently being typed into
        bool isTyping = false; //true while the bot is mid-response


        bool waitingForName = false; //true while waiting for the user to type their name
        bool startupDone = false; //true once the name has been collected and normal chat can begin
        bool askNameAfterTyping = false; //tells the typing timer to trigger the name prompt when it finishes
        bool exitAfterTyping = false; //tells the typing timer to keep input disabled after a goodbye message


        public Form1()
        {
            InitializeComponent();
            test();
            /*SetupDelegateList();   //fill the delegate list with all topic methods
            SetupTypingTimer();    //configure the typing timer
            PlayStartupSound();    //play  cypherr.wav
            ShowWelcomeMessage();  //display the welcome message then prompt for name*/
        }

        
        public void test()
        {
            TaskItem task = new TaskItem();

            task.Title = "Test Task";

            task.Description =
                "Testing from WinForms";

            task.ReminderDate = null;

            task.IsCompleted = false;

            db.AddTask(task);

            MessageBox.Show("Task Added");
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
            typingTimer.Interval = 18;                                      //fire every 18ms - same feel as Thread.Sleep(25) from Part 1
            typingTimer.Tick += new EventHandler(TypingTimer_Tick);         //call TypingTimer_Tick on every tick
        }

        private void TypingTimer_Tick(object sender, EventArgs e) //called every 18ms while the bot is typing
        {
            if (typingIndex < fullTypingText.Length) //still characters left to reveal
            {
                typingLabel.Text = fullTypingText.Substring(0, typingIndex + 1); //reveal one more character into the label
                typingIndex++;                                                    //move to the next character position
                ScrollToBottom();                                                 //keep the chat scrolled to the bottom as text grows
            }
            else //all characters have been revealed
            {
                typingTimer.Stop(); //stop the timer
                isTyping = false;   //mark that typing has finished

                if (exitAfterTyping) //if this was a goodbye message, keep input disabled permanently
                {
                    btnSend.Enabled = false;
                    txtInput.Enabled = false;
                    return;
                }

                if (askNameAfterTyping) //if we need to show the name prompt next
                {
                    askNameAfterTyping = false; //clear the flag so it doesn't trigger again
                    AskForName();               //show the name prompt
                    return;
                }

                //normal case - re-enable input so the user can send their next message
                btnSend.Enabled = true;
                txtInput.Enabled = true;
                txtInput.Focus(); //put the cursor back in the input box automatically
            }
        }

        private void StartTypingEffect(Label label, string fullText) //starts the character-by-character typing effect on a given label
        {
            typingLabel = label;    //store a reference to the label we will be updating each tick
            fullTypingText = fullText; //store the full text to be typed out
            typingIndex = 0;        //start from the very first character
            isTyping = true;     //mark that we are currently typing

            btnSend.Enabled = false;  //disable the send button while typing so messages don't pile up
            txtInput.Enabled = false;  //disable the input box while the bot is typing
            typingTimer.Start();       //start the timer - TypingTimer_Tick will do the rest
        }

        private void AddBotMessage(string message) //adds a bot message to the left side and starts typing it out
        {
            //name label - small bold label showing "Cypherr" above the message
            Label nameLabel = new Label();
            nameLabel.Text = "Cypherr";
            nameLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            nameLabel.ForeColor = Color.FromArgb(56, 189, 248); //cyan colour for the bot name
            nameLabel.AutoSize = true;
            nameLabel.Margin = new Padding(4, 6, 4, 0); //a little space above each message

            //message label - starts empty, typing timer fills it in character by character
            Label messageLabel = new Label();
            messageLabel.Text = "";                              //empty to start - typing effect will fill this
            messageLabel.Font = new Font("Segoe UI", 10f);
            messageLabel.ForeColor = Color.FromArgb(56, 189, 248); //cyan text for bot messages
            messageLabel.AutoSize = true;
            messageLabel.MaximumSize = new Size(panelDisplay.ClientSize.Width - 40, 0); //limit width to roughly half the panel
            messageLabel.Margin = new Padding(4, 0, 4, 4);

            panelDisplay.Controls.Add(nameLabel);    //add name label to the flow panel
            panelDisplay.Controls.Add(messageLabel); //add message label to the flow panel
            ScrollToBottom();

            StartTypingEffect(messageLabel, message); //start typing the response into this label character by character
        }

        private void AskForName() //asks the user to enter their name - called automatically after welcome finishes
        {
            waitingForName = true;  //the next input from the user will be treated as their name
            startupDone = false; //normal chat hasn't started yet

            txtInput.Enabled = true; //enable input so the user can type their name
            btnSend.Enabled = true;
            txtInput.Focus();

            AddBotMessage("Before we begin, what is your name?"); //type the name prompt with the typing effect
        }


        private void AddUserMessage(string message) //adds a user message to the right side instantly
        {
            //get the remembered name for the label, fall back to "You" if not stored yet
            string displayName = "You";
            if (RobotResponses.memory.ContainsKey("name"))
            {
                displayName = RobotResponses.memory["name"]; //use the name we stored in memory
            }

            //name label
            Label nameLabel = new Label();
            nameLabel.Text = displayName;
            nameLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            nameLabel.ForeColor = Color.FromArgb(74, 222, 128); //green colour for the user name
            nameLabel.AutoSize = true;
            nameLabel.Margin = new Padding(4, 6, 4, 0);

            //message label - shown immediately with no typing effect
            Label messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.Font = new Font("Segoe UI", 10f);
            messageLabel.ForeColor = Color.FromArgb(74, 222, 128); //green text for user messages
            messageLabel.AutoSize = true;
            messageLabel.MaximumSize = new Size(panelDisplay.ClientSize.Width - 40, 0);
            messageLabel.Margin = new Padding(4, 0, 4, 4);

            panelDisplay.Controls.Add(nameLabel);    //add to the flow panel
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string userInput = txtInput.Text.Trim(); //get the text from the input box and remove extra spaces


            if (waitingForName)
            {
                AddUserMessage(userInput);
                txtInput.Clear();

                string response = RobotResponses.RememberName(userInput);

                waitingForName = false;
                startupDone = true;

                //AddBotMessage(response);

                // OPTIONAL: re-introduce purpose/help after name
                AddBotMessage($"Hi {userInput}, I can help you learn about cybersecurity. Try asking 'what can you do' or 'help'.");

                return;
            }


            if (userInput == "") //if the user sent nothing, do nothing
            {
                return;
            }

            AddUserMessage(userInput); //display the user's message on the RIGHT side of the chat
            txtInput.Clear();          //clear the input box after sending

            string botResponse = ""; //will hold the bot's response

            // Loop through every delegate in our list and call it with the user's input.
            // If a method returns a non-empty string, it means it matched - use that response and stop.
            string lowercaseInput = userInput.ToLower(); //convert input to lowercase so keywords match regardless of capitalisation

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

            //if none of the methods matched, use the default response
            if (botResponse == "")
            {
                botResponse = RobotResponses.DefaultResponse(); //call the default/fallback response
            }

            //check if the response starts with "EXIT|" which means the user wants to quit
            if (botResponse.StartsWith("EXIT|"))
            {
                string goodbyeMessage = botResponse.Replace("EXIT|", ""); //remove the EXIT| prefix
                AddBotMessage(goodbyeMessage); //show the goodbye message
                txtInput.Enabled = false;   //disable the input box so the user can't type anymore
                btnSend.Enabled = false;    //disable the send button
            }
            else
            {
                AddBotMessage(botResponse); //display the bot's response on the LEFT side of the chat
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

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //if the key pressed was Enter
            {
                e.SuppressKeyPress = true; //suppress the ding sound that TextBox makes on Enter
                button1_Click(sender, e);  //treat it exactly the same as clicking the Send button
            }
        }
    }
}