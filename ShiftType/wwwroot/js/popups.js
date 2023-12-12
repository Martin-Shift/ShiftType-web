
const addQuoteShowButton = document.getElementById('addQuoteButton');

// Get the popup wrapper and form elements
const addQuotePopupWrapper = document.getElementById('editProfilePopupWrapper');
const addQuoteForm = document.getElementById('editProfilePopup');

// Attach a click event listener to the button
addQuoteShowButton.addEventListener('click', function (event) {
    event.preventDefault();

    // Show the edit profile popup
    addQuotePopupWrapper.style.display = 'grid';
    addQuotePopupWrapper.focus();
});

// Optional: Close the popup when the user clicks outside the form
addQuotePopupWrapper.addEventListener('click', function (event) {
    if (event.target === addQuotePopupWrapper) {
        addQuotePopupWrapper.style.display = 'none';
    }
});

// Optional: Close the popup when the 'ESC' key is pressed
document.addEventListener('keydown', function (event) {
    if (event.key === 'Escape' || event.keyCode === 27) {
        addQuotePopupWrapper.style.display = 'none';
    }
});

function downloadExcel() {
    window.location.href = '/account/exportExcel';
}

document.getElementById('submitAdd').addEventListener('click', function () {

    // Create a new FormData object
    var formData = new FormData();

    // Get the form elements by their names and add them to the formData
    var formElements = document.querySelectorAll('#editProfilePopup [name]');
    formElements.forEach(function (element) {

        formData.append(element.name, element.value);

    });

    fetch('/quote/submit', {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (response.ok) {
                document.getElementById('editProfilePopupWrapper').style.display = 'none';
            } else {
                // Handle errors or failed submission
                console.error('Error submitting form data');
            }
        })
        .catch(error => {
            // Handle fetch errors
            console.error('Fetch error:', error);
        });
});


const languageSelectorButton = document.getElementById('languageSelector');

// Get the popup wrapper and form elements
const selectLanguageWrapper = document.getElementById('commandLineWrapper');
const addLanguageForm = document.getElementById('commandLine');

// Attach a click event listener to the button
languageSelectorButton.addEventListener('click', function (event) {
    event.preventDefault();
    selectLanguageWrapper.style.display = 'grid';
    selectLanguageWrapper.focus();
});

// Optional: Close the popup when the user clicks outside the form
selectLanguageWrapper.addEventListener('click', function (event) {
    if (event.target === selectLanguageWrapper) {
        selectLanguageWrapper.style.display = 'none';
    }
});

function loadEntries() {
    let entries = document.querySelectorAll('.languageEntry');
    entries.forEach((entry) => {
        entry.addEventListener('mouseenter', function () {
            this.classList.add('active');
        });

        entry.addEventListener('mouseleave', function () {
            this.classList.remove('active');
        });

        entry.addEventListener('click', function () {
            const languageName = this.querySelector('div:last-child').textContent;
            const languageParagraph = document.getElementById('language');
            languageParagraph.textContent = languageName;
            selectLanguageWrapper.style.display = "none";
            resetTest();
            getTest(globalModifiers);
        });
    });
}

document.addEventListener('keydown', function (event) {
    if (event.key === 'Escape' || event.keyCode === 27) {
        selectLanguageWrapper.style.display = 'none';
    }
});
var searchInput = document.getElementById('searchInput');
searchInput.addEventListener('input', function () {
    const searchString = this.value.trim(); 
    fetch(`/language/change`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(searchString),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(data => {
            $('#languageList').html(data);
            loadEntries();
        })
        .catch(error => {
            // Handle errors here
            console.error('There was a problem with the fetch operation:', error);
        });
       
});
loadEntries();


const customConfigButton = document.getElementById('customTimeConfigButton');

// Get the popup wrapper and form elements
const customTestDurationPopupWrapper = document.getElementById('customTestDurationPopupWrapper');
const customTestDurationPopup = document.getElementById('customTestDurationPopup');

// Attach a click event listener to the button
customConfigButton.addEventListener('click', function (event) {
    event.preventDefault();

    // Show the edit profile popup
    customTestDurationPopupWrapper.style.display = 'grid';
    customTestDurationPopupWrapper.focus();
});

// Optional: Close the popup when the user clicks outside the form
customTestDurationPopupWrapper.addEventListener('click', function (event) {
    if (event.target === customTestDurationPopupWrapper) {
        customTestDurationPopupWrapper.style.display = 'none';
    }
});

// Optional: Close the popup when the 'ESC' key is pressed
document.addEventListener('keydown', function (event) {
    if (event.key === 'Escape' || event.keyCode === 27) {
        customTestDurationPopupWrapper.style.display = 'none';
    }
});
document.getElementById("customTestDurationPopupSubmit").addEventListener("click", function () {


    var text = document.getElementById("customTestDuration").value;
    try {
        var time = parseInt(text);
        globalModifiers.TimeAmount = time;
        resetTest();
        getTest(globalModifiers);
        document.getElementById('time-remaining').innerText = time;
        customTestDurationPopupWrapper.style.display = 'none';
    }
    catch (ex)
    {
        console.error(ex);
    }
});


const customWordsConfigButton = document.getElementById('customWordConfigButton');

// Get the popup wrapper and form elements
const customWordCountPopupWrapper = document.getElementById('customWordAmountPopupWrapper');
const customWordCountPopup = document.getElementById('customWordAmountPopup');

// Attach a click event listener to the button
customWordsConfigButton.addEventListener('click', function (event) {
    event.preventDefault();

    // Show the edit profile popup
    customWordCountPopupWrapper.style.display = 'grid';
    customWordCountPopupWrapper.focus();
});

// Optional: Close the popup when the user clicks outside the form
customWordCountPopupWrapper.addEventListener('click', function (event) {
    if (event.target === customWordCountPopupWrapper) {
        customWordCountPopupWrapper.style.display = 'none';
    }
});

// Optional: Close the popup when the 'ESC' key is pressed
document.addEventListener('keydown', function (event) {
    if (event.key === 'Escape' || event.keyCode === 27) {
        customWordCountPopupWrapper.style.display = 'none';
    }
});
document.getElementById("customWordCountPopupSubmit").addEventListener("click", function () {

    try {
        var text = document.getElementById("customWordCount").value;
        console.log(text);
        var time = parseInt(text);
        globalModifiers.WordCount = time;
        getTest(globalModifiers);
        customWordCountPopupWrapper.style.display = 'none';
    }
    catch (ex) {
        console.error(ex);
    }
});