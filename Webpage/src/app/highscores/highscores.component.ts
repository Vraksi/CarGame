import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HighscoreService } from '../service/highscores.service';
import { Highscore } from '../models/highscore';


@Component({
  selector: 'app-highscores',
  templateUrl: './highscores.component.html',
  styleUrls: ['./highscores.component.css']
})
export class HighscoresComponent implements OnInit {

  highscores: Highscore[];
  highscore: Highscore;
  selectedHighscore: Highscore;

  constructor(private highscoreService: HighscoreService) { }

  ngOnInit(): void {
    this.highscore = { highscoreId: 0, 'name': '', score: 0 };
    this.getHighscores();
  }
  getHighscores(): void {
    this.highscoreService.getHighscores()
      .subscribe(highscores => this.highscores = highscores);
  }
  onSelect(highscore: Highscore): void {
    this.selectedHighscore = highscore;
  }
}
