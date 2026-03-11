export interface ExamQuestion {
  id: number;
  questionNo: number;
  questionText: string;
  choiceA: string;
  choiceB: string;
  choiceC: string;
  choiceD: string;
}

export interface ExamAnswerRequest {
  questionId: number;
  selectedChoice: string;
}

export interface ExamSubmitRequest {
  examineeName: string;
  answers: ExamAnswerRequest[];
}

export interface ExamAnswerResult {
  questionNo: number;
  questionText: string;
  choiceA: string;
  choiceB: string;
  choiceC: string;
  choiceD: string;
  selectedChoice: string;
  correctChoice: string;
  isCorrect: boolean;
}

export interface ExamResult {
  sessionId: number;
  examineeName: string;
  score: number;
  totalQuestions: number;
  scoreText: string;
  takenAt: string;
  results: ExamAnswerResult[];
}
