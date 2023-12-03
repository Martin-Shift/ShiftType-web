
function showTestType(testType) {
    const testTypeDivs = {
        time: document.querySelector('.time'),
        words: document.querySelector('.wordCount'),
        quote: document.querySelector('.quoteLength'),
        zen: document.querySelector('.zen'),
        custom: document.querySelector('.customText'),
    };

    const timeConfigButtons = document.querySelectorAll('.time .textButton');
    const wordCountButtons = document.querySelectorAll('.wordCount .textButton');
    const quoteLengthButtons = document.querySelectorAll('.quoteLength .textButton');

    for (const type in testTypeDivs) {
        if (type === testType) {
            testTypeDivs[type].classList.remove('hidden');
        } else {
            testTypeDivs[type].classList.add('hidden');
        }
    }

    if (testType === 'time') {
        timeConfigButtons[0].classList.add('active');
        wordCountButtons.forEach(button => button.classList.remove('active'));
        quoteLengthButtons.forEach(button => button.classList.remove('active'));
    } else if (testType === 'words') {
        wordCountButtons[0].classList.add('active');
        timeConfigButtons.forEach(button => button.classList.remove('active'));
        quoteLengthButtons.forEach(button => button.classList.remove('active'));
    } else if (testType === 'quote') {
        quoteLengthButtons[0].classList.add('active');
        timeConfigButtons.forEach(button => button.classList.remove('active'));
        wordCountButtons.forEach(button => button.classList.remove('active'));
    }
}

document.getElementById('timeSwitch').addEventListener('click', () => {
    showTestType('time');
    const punctuationAndNumbers = document.querySelector('.puncAndNum');
    punctuationAndNumbers.style.display = 'flex';
    document.getElementById('timeSwitch').classList.add('active');
    document.getElementById('wordSwitch').classList.remove('active');
    document.getElementById('quoteSwitch').classList.remove('active');
});

document.getElementById('wordSwitch').addEventListener('click', () => {
    showTestType('words');
    document.getElementById('wordSwitch').classList.add('active');
    document.getElementById('timeSwitch').classList.remove('active');
    document.getElementById('quoteSwitch').classList.remove('active');
    const punctuationAndNumbers = document.querySelector('.puncAndNum');
    const wordCountButtons = document.querySelector('.wordCount');
    const quoteLengthButtons = document.querySelector('.quoteLength');
    punctuationAndNumbers.style.display = 'flex';

});

document.getElementById('quoteSwitch').addEventListener('click', () => {
    showTestType('quote');
    document.getElementById('quoteSwitch').classList.add('active');
    document.getElementById('timeSwitch').classList.remove('active');
    document.getElementById('wordSwitch').classList.remove('active');
    const punctuationAndNumbers = document.querySelector('.puncAndNum');
    punctuationAndNumbers.style.display = 'none';
});

document.getElementById('zenSwitch').addEventListener('click', () => {
    showTestType('zen');
});

document.getElementById('customSwitch').addEventListener('click', () => {
    showTestType('custom');
});
document.querySelectorAll('.time .textButton').forEach(button => {
    button.addEventListener('click', () => {
        document.querySelectorAll('.time .textButton').forEach(btn => {
            btn.classList.remove('active');
        });
        button.classList.add('active');
    });
});

document.querySelectorAll('.wordCount .textButton').forEach(button => {
    button.addEventListener('click', () => {
        document.querySelectorAll('.wordCount .textButton').forEach(btn => {
            btn.classList.remove('active');
        });
        button.classList.add('active');
    });
});
document.querySelectorAll('.quoteLength .textButton').forEach(button => {
    button.addEventListener('click', () => {
        document.querySelectorAll('.quoteLength .textButton').forEach(btn => {
            btn.classList.remove('active');
        });
        button.classList.add('active');
    });
});
const punctuationButton = document.querySelector('.puncAndNum .punctuationMode');
const numbersButton = document.querySelector('.puncAndNum .numbersMode');

punctuationButton.addEventListener('click', () => {
    if (punctuationButton.classList.contains('active')) {
        punctuationButton.classList.remove('active');
    } else {
        punctuationButton.classList.add('active');
    }

    // TODO logic for handling punctuation mode
});

numbersButton.addEventListener('click', () => {
    if (numbersButton.classList.contains('active')) {
        numbersButton.classList.remove('active');
    } else {
        numbersButton.classList.add('active');
    }

    // TODO logic for handling numbers mode
});

// Function to update caret position based on currentWordIndex and currentLetterIndex

