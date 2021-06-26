let cardElement = document.querySelector("#questionCard");
let footerElement = document.querySelector("#footer");

function displayExam() {
    let startBtnElement = document.querySelector("#startBtn");
    startBtnElement.style.display = 'none';
    cardElement.style.display = 'block';
    footerElement.style.display = 'block';
}

function populateExam(examToPopulate) {
    let questionObj = examToPopulate.getCurrentQuestion();
    var elementQuestion = document.getElementById("questionText");//p
    elementQuestion.innerHTML = questionObj.QuestionText;
    let elementProgress = document.getElementById("progress");//progress bar
    elementProgress.innerHTML =
        `Question ${examToPopulate.currentIndex + 1} of ${examToPopulate.questions.length}`
    let answersDivElement = document.getElementById("answersDiv");//Buttons Div
    answersDivElement.innerText = '';
    questionObj.Answers.forEach((choiceText, index) => { //Create Choices Buttons
        let newBtn = createBtnElement(choiceText, index);
        if (choiceText === examToPopulate.userAnswers[examToPopulate.currentIndex]) {
            newBtn.classList.add("active");
        }
        newBtn.setAttribute('type', 'button');
        newBtn.onclick = function () {
            //Check add to answers
            examToPopulate.userAnswers[examToPopulate.currentIndex] = choiceText;
            saveProgress(this);

            //Go Next
            if (examToPopulate.currentIndex != examToPopulate.questions.length - 1) {
                examToPopulate.goNext();
                populateExam(examToPopulate);
            } else {
                let allBtns = answersDivElement.querySelectorAll("button");
                allBtns.forEach(btn => {
                    if (btn.classList.contains("active"))
                        btn.classList.remove("active");
                });
                this.classList.add("active");
            }

        };
        answersDivElement.appendChild(newBtn);
    });
    let btnNextElement = document.getElementById("next");
    setBtnStyle(btnNextElement, examToPopulate.currentIndex, examToPopulate.questions.length - 1)
    btnNextElement.onclick = function () {
        //NEXT Button Function
        if (examToPopulate.userAnswers[examToPopulate.currentIndex] == null) {
            examToPopulate.userAnswers[examToPopulate.currentIndex] = '';
        }
        if (!isBtnDisabled(this)) {
            saveProgress(this);
            examToPopulate.goNext();
            populateExam(examToPopulate);
        }
    }
    let btnPrevElement = document.getElementById("prev");
    setBtnStyle(btnPrevElement, examToPopulate.currentIndex, 0)
    btnPrevElement.onclick = function () {
        if (!isBtnDisabled(this)) {
            saveProgress(this);
            examToPopulate.goPrev();
            populateExam(examToPopulate);
        }
        //PREV Button Function
        if (examToPopulate.userAnswers[examToPopulate.currentIndex] == null) {
            examToPopulate.userAnswers[examToPopulate.currentIndex] = ''
        }
    }
}

function setBtnStyle(btnElement, index1, index2) {
    if (index1 == index2) {
        btnElement.classList.add('disabled');
    } else if (btnElement.classList.contains('disabled')) {
        btnElement.classList.remove('disabled')
    }
}

function isBtnDisabled(btnElement) {
    if (btnElement.classList.contains('disabled')) {
        return true;
    } else {
        return false;
    }
}

function createBtnElement(text, index) {
    let elementBtn = document.createElement('button');
    elementBtn.setAttribute('id', index);
    elementBtn.setAttribute('class', 'list-group-item')
    elementBtn.classList.add('list-group-item-action');
    // elementBtn.classList.add('btn-outline-primary');
    elementBtn.classList.add('col-10');

    let spanString = `<span id="choice${index}">${text}</span>`
    elementBtn.innerHTML = spanString;
    return elementBtn;
}

