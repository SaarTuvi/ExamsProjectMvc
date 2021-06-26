let questionsDiv = document.getElementById("questions");
let answerCounter = 1;
let questionCounter = 1;
let questionsList = [];



function createNewQuestionCard(isFirst) {
    answerCounter = 1;
    let cardDiv = document.createElement("div");
    cardDiv.className = "card border-dark mb-3"
    cardDiv.id = `questionCard${questionCounter}`;
    let cardHeader = document.createElement("div");
    cardHeader.className = "card-header";

    let cardHeaderRow = document.createElement("div");
    cardHeaderRow.className = "row"

    let headerInputCol = document.createElement("div");
    headerInputCol.className = "col";

    let questionTextInput = document.createElement("input");
    questionTextInput.type = "text";
    questionTextInput.className = "form-control";
    questionTextInput.id = "questionInputText";
    questionTextInput.placeholder = "Question Text";

    headerInputCol.appendChild(questionTextInput);

    if (!isFirst) {
        headerInputCol.className = "col-11";
        let headerDeleteCol = document.createElement("div")
        headerDeleteCol.className = "col-1";
        let deleteQuestionElement = document.createElement('button');
        deleteQuestionElement.type = "button";
        deleteQuestionElement.innerHTML = "X";
        deleteQuestionElement.className = "btn btn-danger";
        deleteQuestionElement.addEventListener('click', function (e) {
            console.log(e);
            e.target.parentNode.parentNode.parentNode.parentNode.remove();
        });

        headerDeleteCol.appendChild(deleteQuestionElement);

        cardHeaderRow.appendChild(headerInputCol);
        cardHeaderRow.appendChild(headerDeleteCol);

    } else {
        cardHeaderRow.appendChild(headerInputCol);
    }

    cardHeader.appendChild(cardHeaderRow);

    let cardBody = document.createElement("div");
    cardBody.className = "card-body";

    for (var x = 0; x < 2; x++) {
        let formCheck = document.createElement("div");
        formCheck.className = "form-check";

        let formRow = document.createElement("div");
        formRow.className = "form-row";

        let radioInput = document.createElement("input");
        radioInput.type = "radio";
        radioInput.className = "form-check-input";
        radioInput.name = `question${questionCounter}Radio`;
        radioInput.id = `option${answerCounter}`;
        radioInput.value = answerCounter;

        let questionOptionInput = document.createElement("input");
        questionOptionInput.type = "text";
        questionOptionInput.className = "form-control";
        questionOptionInput.id = `optionText${answerCounter}`;
        questionOptionInput.placeholder = `Option ${answerCounter}`;
        questionOptionInput.style = "width:75%";

        formRow.appendChild(radioInput);
        formRow.appendChild(questionOptionInput);

        formCheck.appendChild(formRow);
        cardBody.appendChild(formCheck);
        answerCounter++;
    }

    let newFormCheck = createNewFormCheck(cardBody, questionCounter, answerCounter);
    cardBody.appendChild(newFormCheck);

    cardDiv.appendChild(cardHeader);
    cardDiv.appendChild(cardBody);

    let addQuestionBtn = document.getElementById('addQuestionBtn');
    questionsDiv.insertBefore(cardDiv, addQuestionBtn);
}

function createNewFormCheck(cardBody, questionIndex, answerIndex) {
    let newFormCheck = document.createElement("div");
    newFormCheck.className = "form-check ";

    let newFormRow = document.createElement("div");
    newFormRow.className = "form-row";

    let newOptionInput = document.createElement('input');
    newOptionInput.type = "text";
    newOptionInput.className = "form-control";
    newOptionInput.placeholder = "Add Another";
    newOptionInput.id = `optionText${answerIndex}`;
    newOptionInput.style = "width:75%"
    newOptionInput.onblur = function () {
        let textValue = newOptionInput.value;
        if (textValue !== null && textValue !== "") {
            let radioElement = document.createElement('input');
            radioElement.type = "radio";
            radioElement.className = "form-check-input";
            radioElement.name = `question${questionIndex}Radio`;
            radioElement.id = `option${answerIndex}`;
            radioElement.value = answerIndex;

            newFormRow.insertBefore(radioElement, newOptionInput);

            let deleteAnswerBtn = document.createElement('button');
            deleteAnswerBtn.type = 'button';
            deleteAnswerBtn.className = "btn btn-danger";
            deleteAnswerBtn.innerText = "X";
            deleteAnswerBtn.addEventListener('click', function (e) {
                e.currentTarget.parentNode.parentNode.remove();
            });

            newFormRow.appendChild(deleteAnswerBtn);
            answerIndex++;

            newInput = createNewFormCheck(cardBody);
            cardBody.appendChild(newInput);

            newOptionInput.onblur = null;
        }
    }
    newFormRow.appendChild(newOptionInput);
    newFormCheck.appendChild(newFormRow);

    return newFormCheck;
}

function addQuestion() {
        questionCounter++;
        createNewQuestionCard(false);
}

function checkQuestionFilled() {
    let questionElement = document.getElementById(`questionCard${questionCounter}`);
    let questionText = questionElement.querySelector("#questionInputText").value;
    if (questionText === "") {
        alert("Question Text Cannot Be Empty");
        return false;
    }

    let answersElements = questionElement.querySelectorAll("input[type=text]");
    if (answersElements.length <= 3) {
        alert("Must Enter At Least 2 Options")
        return false;
    }

    answersElements.forEach(element => {
        if (element.value === "") {
            alert("Answer Choice Cannot Be Null");
            return false;
        }
    })

    let radioElements = questionElement.querySelector("input[type=radio]:checked");
    if (radioElements === null) {
        alert("Please Select The Correct Answer");
        return false;
    }



    return true;
}

