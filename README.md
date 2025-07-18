
# 🔐 Token Grabber - Cybersecurity Awareness Project

> 📁 **Project Name**: Token Grabber  
> 👨‍💻 Author: Nur Mohammad Rafi  
> 🛡️ **Purpose**: Educational demonstration on Discord token security, misuse awareness, and detection.

---

## 📌 Project Overview

This project is designed **for educational purposes only** to demonstrate how **Discord tokens** can be compromised if users are not cautious with third-party links, tools, or untrusted applications.

It helps users understand:
- How token stealing works.
- How malicious tools are created and disguised.
- How to protect accounts from unauthorized access.
- How antivirus software detects such activity.

---

## ⚙️ How It Works (Educational Explanation)

The tool simulates how a Discord token grabber might function by:

1. **Reading the `Local Storage` and `LevelDB` folders** of Discord to find token strings.
2. **Extracting stored Discord tokens** from known paths (e.g., `AppData\Roaming\Discord\Local Storage`).
3. **Sending the stolen token** to a webhook URL (set by attacker) using a simple POST request.

---

## 🧠 Educational Value

- Teaches students **how social engineering and malware operate**.
- Demonstrates **token storage locations** and how insecure software can exploit them.
- Shows how to **secure Discord accounts** using 2FA, token reset, and safe browsing habits.

---

## 🚀 How to Run (Simulation Only)

> ⚠️ Run this ONLY in a **controlled test environment (like a virtual machine)**.

1. Clone this repository:
   ```bash
   git clone https://github.com/ostwrafi/Token-Grabber
   cd Token-Grabber
   ```

2. Edit the  file to insert your **test webhook URL**:
   ```
   WEBHOOK_URL = "https://your-test-webhook.com"
   ```


4. It will simulate reading tokens and sending them to the webhook.

---

## 🔐 Protection Tips

To avoid token theft:

- Never share files from untrusted sources.
- Enable **2-Factor Authentication** in Discord.
- Regularly check **active sessions** and **log out of old devices**.
- Use a reliable **antivirus and firewall**.
- Be cautious about installing **modded Discord clients or "enhancers."**

---

## 📄 License

```
This project is licensed for educational, ethical hacking, and cybersecurity awareness purposes only.

© 2025 Nur Mohammad Rafi – All rights reserved.
```

---

## 🧑‍🏫 Disclaimer

> ⚠️ This project is strictly for **education and awareness**.  
> Do not use it to access or steal unauthorized accounts.  
> Any misuse of this tool is strictly prohibited and may be illegal.  
> The author is not responsible for any misuse of the project.

---

## 🎓 Author

**Nur Mohammad Rafi**  
Cybersecurity Enthusiast  
GitHub: [@ostwrafi](https://github.com/ostwrafi)
