# TranslateCmdPal

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

TranslateCmdPal is the command palette version of [DeepLTranslatorPowerToys](https://github.com/patcher454/DeepLTranslatorPowerToys).

Seamlessly translate text directly from the PowerToys command palette.

<img width="821" height="506" alt="Extension Introduce" src="https://github.com/user-attachments/assets/7b439b37-7d0e-4e09-acf9-666c097be616" />

## ✨ Features

-   **Instant Translation**: Translate text directly from the **Command Palette** (`Win` + `Alt` + `Space`).
-   **Clipboard Ready**: The translated text is copied to your clipboard, ready to be pasted with `Ctrl + V`.
-   **Translation History**: View your past translations and copy them again directly from the **Command Palette** window.
-   **Default Language**: Set a default target language to avoid typing the language code every time.
-   **Broad Language Support**: Utilizes all languages supported by the DeepL API.

## ⚙️ Prerequisites

Before you begin, ensure you have the following:

1.  **PowerToys**: A set of utilities for Windows power users. [Install it from here](https://learn.microsoft.com/en-us/windows/powertoys/install).
2.  **DeepL API Key**: An authentication key to use the DeepL translation service.
    -   Sign up [DeepL API page](https://www.deepl.com/pro-api?cta=header-pro-api).
    -   Find your **Authentication Key** on your [DeepL Account page](https://www.deepl.com/your-account/keys).

## 🚀 Installation & Setup

1.  **Install the Plugin**
    -   Download and install TranslateCmdPal from the [Microsoft Store](https://apps.microsoft.com/detail/9P02SXVHRJV8).

2.  **Configure API Key and Settings**
    <img width="781" height="82" alt="image" src="https://github.com/user-attachments/assets/45ded910-1c11-4f85-a1e6-c7075370e77e" /> 
    <img width="780" height="471" alt="image" src="https://github.com/user-attachments/assets/e59781b7-7155-4a4e-a9bd-ee670a9d6a9a" />

## 💡 How to Use

1.  **Open Command palette**
    -   Press `Win` + `Alt` + `Space` to launch the Command palette window.
2.  **Run TranslateCmdPal from the list**
3.  **Enter Your Translation Query**
    -   **Format**: `{target_language_code} {text_to_translate}`
        -   **To specify a language:**
            ```
            ko hello world
            ```
        -   **To use the default language (omit the code):**
            ```
            안녕하세요!
            ```
            *(This assumes your Default Target Language is set to `en`)*

3.  **Get the Result**
    -   Press `Enter`. The translated text is copied to your clipboard for pasting with `Ctrl + V`, and the result is saved to your translation history.

## 🌐 Supported Languages and Codes

This is a list of languages supported by the DeepL API.

| Code | Language | Code | Language |
| :--- | :--- | :--- | :--- |
| `ar` | Arabic | `it` | Italian |
| `bg` | Bulgarian | `ja` | Japanese |
| `cs` | Czech | `ko` | Korean |
| `da` | Danish | `lt` | Lithuanian |
| `de` | German | `lv` | Latvian |
| `el` | Greek | `nb` | Norwegian (Bokmål) |
| `en` | English (unspecified) | `nl` | Dutch |
| `en-gb` | English (British) | `pl` | Polish |
| `en-us` | English (American) | `pt` | Portuguese (unspecified) |
| `es` | Spanish | `pt-br` | Portuguese (Brazilian) |
| `et` | Estonian | `pt-pt` | Portuguese (non-Brazilian) |
| `fi` | Finnish | `ro` | Romanian |
| `fr` | French | `ru` | Russian |
| `hu` | Hungarian | `sk` | Slovak |
| `id` | Indonesian | `sl` | Slovenian |
| `sv` | Swedish | `tr` | Turkish |
| `uk` | Ukrainian | `zh` | Chinese (simplified) |
