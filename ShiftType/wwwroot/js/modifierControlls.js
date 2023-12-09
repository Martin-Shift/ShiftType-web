function createModifiers(testType, additionalModifiers = {}) {
    const modifiers = {
        TestType: testType,
        TimeAmount: null,
        WordCount: null,
        CustomText: null,
        IsNumbers: false,
        IsSymbols: false,
        Language: document.getElementById('language').textContent,
        QuoteType: null,
        ...additionalModifiers,
    };

    return modifiers;
}

document.getElementById('timeSwitch').addEventListener('click', () => {
    const timeConfig = 15; // Replace this value with your logic
    globalModifiers.TimeAmount = 15
    globalModifiers.TestType = 0;
    resetTest();
    getTest(globalModifiers);
    document.getElementById('time-remaining').innerText = timeConfig;
});

document.getElementById('wordSwitch').addEventListener('click', () => {
    globalModifiers.WordCount = 10;
    globalModifiers.TestType = 1;
    globalModifiers.TimeAmount = null;
    resetTest();
    getTest(globalModifiers);
});

document.getElementById('quoteSwitch').addEventListener('click', () => {
    const quoteTypeButton = document.querySelector('.quoteLength .textButton.active');
    let quoteType = null;

    if (quoteTypeButton) {
        quoteType = parseInt(quoteTypeButton.getAttribute('quotelength'));
    }
    globalModifiers.TimeAmount = null;
    globalModifiers.TestType = 2;
    globalModifiers.QuoteType = quoteType;
    resetTest();
    getTest(globalModifiers);
});
const timeConfigButtons = document.querySelectorAll('.time .textButton');
timeConfigButtons.forEach((button) => {
    button.addEventListener('click', () => {
        globalModifiers.TimeAmount = parseInt(button.getAttribute('timeconfig'));
        resetTest();
        getTest(globalModifiers);
        document.getElementById('time-remaining').innerText = timeConfig;
    });
});

const wordCountButtons = document.querySelectorAll('.wordCount .textButton');
wordCountButtons.forEach((button) => {
    button.addEventListener('click', () => {
        globalModifiers.WordCount = parseInt(button.getAttribute('wordcount'));
        resetTest();
        getTest(globalModifiers);
        document.getElementById('time-remaining').innerText = globalModifiers.TimeAmount;
    });
});

const quoteLengthButtons = document.querySelectorAll('.quoteLength .textButton');
quoteLengthButtons.forEach((button) => {
    button.addEventListener('click', () => {
        globalModifiers.QuoteType = parseInt(button.getAttribute('quotelength'));
        resetTest();
        getTest(modifiers);
    });
});
function getTest(modifiers) {
    fetch('type/getTest', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(modifiers),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.json();
        })
        .then(data => {
            const test = data.test;
            
            wordsArray = test.split(/\s+/);
           
            // Add a space character at the end of each word
            wordsArray = wordsArray.map(word => word + ' ');
            wordsArray[wordsArray.length - 1] = wordsArray[wordsArray.length - 1].slice(0, -1);
            words = [...wordsArray];
            const wordsContainer = document.querySelector('#words');
            wordsContainer.innerHTML = '';
            wordsArray.forEach(word => {
                const wordElement = document.createElement('div');
                wordElement.classList.add('word');

                const letters = word.split('');
                letters.forEach(letter => {
                    const letterElement = document.createElement('letter');
                    letterElement.textContent = letter;
                    wordElement.appendChild(letterElement);
                });
                wordsContainer.appendChild(wordElement);
            });
            globalModifiers = modifiers;

        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error.message);
        });
}


punctuationButton.addEventListener('click', () => {
    globalModifiers.IsSymbols = punctuationButton.classList.contains("active");
    resetTest();
    getTest(globalModifiers);
});
numbersButton.addEventListener('click', () => {
    globalModifiers.IsNumbers = numbersButton.classList.contains("active");
    resetTest();
    getTest(globalModifiers);
});