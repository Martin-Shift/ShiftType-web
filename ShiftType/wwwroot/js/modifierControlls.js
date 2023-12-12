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

document.getElementById('zenSwitch').addEventListener('click', () => {
    globalModifiers.TestType = 3;
    globalModifiers.TimeAmount = null;
    globalModifiers.WordCount = null;
    resetTest();
    getTest(globalModifiers);
});
const timeConfigButtons = document.querySelectorAll('.time .timebutton');
timeConfigButtons.forEach((button) => {
    button.addEventListener('click', () => {
        globalModifiers.TimeAmount = parseInt(button.getAttribute('timeconfig'));
        resetTest();
        getTest(globalModifiers);
        document.getElementById('time-remaining').innerText = timeConfig;
    });
});

const wordCountButtons = document.querySelectorAll('.wordCount .wordbutton');
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
        getTest(globalModifiers);
    });
});
function getTest(modifiers) {
    modifiers.Language = document.getElementById('language').textContent;
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
function updateModifiers(mods) {
    if (mods.TimeAmount === 10) {
        document.getElementById('timeAmountButton10').classList.add('active');
    } else {
        document.getElementById('timeAmountButton10').classList.remove('active');
    }

    if (mods.TimeAmount === 30) {
        document.getElementById('timeAmountButton30').classList.add('active');
    } else {
        document.getElementById('timeAmountButton30').classList.remove('active');
    }

    if (mods.TimeAmount === 60) {
        document.getElementById('timeAmountButton60').classList.add('active');
    } else {
        document.getElementById('timeAmountButton60').classList.remove('active');
    }

    if (mods.TimeAmount === 120) {
        document.getElementById('timeAmountButton120').classList.add('active');
    } else {
        document.getElementById('timeAmountButton120').classList.remove('active');
    }

    if (mods.TestType === 0) {
        document.getElementById('timeSwitch').classList.add('active');
    } else {
        document.getElementById('timeSwitch').classList.remove('active');
    }

    if (mods.TestType === 1) {
        document.getElementById('wordSwitch').classList.add('active');
    } else {
        document.getElementById('wordSwitch').classList.remove('active');
    }

    if (mods.TestType === 2) {
        document.getElementById('quoteSwitch').classList.add('active');
    } else {
        document.getElementById('quoteSwitch').classList.remove('active');
    }
    if (mods.QuoteType === 0) {
        document.getElementById('quotelength0').classList.add('active');
    } else {
        document.getElementById('quotelength0').classList.remove('active');
    }

    if (mods.QuoteType === 1) {
        document.getElementById('quotelength1').classList.add('active');
    } else {
        document.getElementById('quotelength1').classList.remove('active');
    }

    if (mods.QuoteType === 2) {
        document.getElementById('quotelength2').classList.add('active');
    } else {
        document.getElementById('quotelength2').classList.remove('active');
    }

    if (mods.QuoteType === 3) {
        document.getElementById('quotelength3').classList.add('active');
    } else {
        document.getElementById('quotelength3').classList.remove('active');
    }

    if (mods.QuoteType === 4) {
        document.getElementById('quotelength4').classList.add('active');
    } else {
        document.getElementById('quotelength4').classList.remove('active');
    }

    if (mods.WordCount === 10) {
        document.getElementById('wordCountButton10').classList.add('active');
    } else {
        document.getElementById('wordCountButton10').classList.remove('active');
    }

    if (mods.WordCount === 25) {
        document.getElementById('wordCountButton25').classList.add('active');
    } else {
        document.getElementById('wordCountButton25').classList.remove('active');
    }

    if (mods.WordCount === 50) {
        document.getElementById('wordCountButton50').classList.add('active');
    } else {
        document.getElementById('wordCountButton50').classList.remove('active');
    }

    if (mods.WordCount === 100) {
        document.getElementById('wordCountButton100').classList.add('active');
    } else {
        document.getElementById('wordCountButton100').classList.remove('active');
    }

    if (mods.IsNumbers) {
        document.getElementById('numbersSwitch').classList.add('active');
    } else {
        document.getElementById('numbersSwitch').classList.remove('active');
    }
    if (mods.IsSymbols) {
        document.getElementById('punctuationSwitch').classList.add('active');
    } else {
        document.getElementById('punctuationSwitch').classList.remove('active');
    }
}