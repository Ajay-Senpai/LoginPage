const themes = [
    {
        background: "#1A1A2E",
        color: "#FFFFFF",
        primaryColor: "#0F3460"
    },
    {
        background: "#461220",
        color: "#FFFFFF",
        primaryColor: "#E94560"
    },
    {
        background: "#192A51",
        color: "#FFFFFF",
        primaryColor: "#967AA1"
    },
    {
        background: "#F7B267",
        color: "#000000",
        primaryColor: "#F4845F"
    },
    {
        background: "#F25F5C",
        color: "#000000",
        primaryColor: "#642B36"
    },
    {
        background: "#231F20",
        color: "#FFF",
        primaryColor: "#BB4430"
    }
];

const setTheme = (theme) => {
    const root = document.querySelector(":root");
    root.style.setProperty("--background", theme.background);
    root.style.setProperty("--color", theme.color);
    root.style.setProperty("--primary-color", theme.primaryColor);
    root.style.setProperty("--glass-color", theme.glassColor);
};

const displayThemeButtons = () => {
    const btnContainer = document.querySelector(".theme-btn-container");
    themes.forEach((theme) => {
        const div = document.createElement("div");
        div.className = "theme-btn";
        div.style.cssText = `background: ${theme.background}; width: 25px; height: 25px`;
        btnContainer.appendChild(div);
        div.addEventListener("click", () => setTheme(theme));
    });
};

displayThemeButtons();

const button = document.getElementById('button');
const username = document.getElementById('username').value;
const password = document.getElementById('password').value;
const email = document.getElementById('email').value;

function login() {
    // API endpoint for login
    const apiUrl = 'https://localhost:7277/api/UserData/Post';
  
    // Data to be sent in the request body
    const requestBody = {
      username: username,
      password: password,
      email: email
    };
  
    // Make a POST request to the login API
    fetch(apiUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
      })
      .then(data => {
        // Assuming the API responds with a success flag and a token
        if (data.success) {
          // Redirect to the next page (adjust the URL accordingly)
          window.location.href = 'ChatApp.html';
        } else {
          // Handle unsuccessful login (e.g., show an error message)
          alert('Invalid username or password');
        }
      })
      .catch(error => {
        // Handle any errors that occurred during the fetch
        console.error('Fetch error:', error);
      });
  }

  button.addEventListener("click", function() {
    login();
});

// var username = "user123";
// var password = "password123";
// var email = "user@example.com";

// var data = {
//     username: username,
//     password: password,
//     email: email
// };

// var xhr = new XMLHttpRequest();
// xhr.open("POST", 'https://localhost:7277/api/UserData/Post', true);
// xhr.setRequestHeader('Content-Type', 'application/json');
// xhr.send(JSON.stringify(data));