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
    const modifiers = createModifiers(0, { TimeAmount: timeConfig });
    resetTest();
    getTest(modifiers);
    document.getElementById('time-remaining').innerText = timeConfig;
});

document.getElementById('wordSwitch').addEventListener('click', () => {
    const wordCount = 10;
    const modifiers = createModifiers(1, { WordCount: wordCount });
    resetTest();
    getTest(modifiers);
});

document.getElementById('quoteSwitch').addEventListener('click', () => {
    const quoteTypeButton = document.querySelector('.quoteLength .textButton.active');
    let quoteType = null;

    if (quoteTypeButton) {
        quoteType = parseInt(quoteTypeButton.getAttribute('quotelength'));
    }

    const modifiers = createModifiers(2, { QuoteType: quoteType });
    resetTest();
    getTest(modifiers);
});
const timeConfigButtons = document.querySelectorAll('.time .textButton');
timeConfigButtons.forEach((button) => {
    button.addEventListener('click', () => {
        const timeConfig = parseInt(button.getAttribute('timeconfig'));
        const modifiers = createModifiers(0, { TimeAmount: timeConfig });
        resetTest();
        getTest(modifiers);
        document.getElementById('time-remaining').innerText = timeConfig;
    });
});

const wordCountButtons = document.querySelectorAll('.wordCount .textButton');
wordCountButtons.forEach((button) => {
    button.addEventListener('click', () => {
        const wordCount = parseInt(button.getAttribute('wordcount'));
        const modifiers = createModifiers(1, { WordCount: wordCount });
        resetTest();
        getTest(modifiers);
        document.getElementById('time-remaining').innerText = globalModifiers.TimeAmount;
    });
});

const quoteLengthButtons = document.querySelectorAll('.quoteLength .textButton');
quoteLengthButtons.forEach((button) => {
    button.addEventListener('click', () => {
        const quoteType = parseInt(button.getAttribute('quotelength'));
        const modifiers = createModifiers(2, { QuoteType: quoteType });
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
            wordsArray.pop();
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