import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { LanguageService as APILanguageService, Language as APILanguage } from '@app/core/api/generated';
import { Observable } from 'rxjs';

export interface Language {
  id: number;
  name: string;
  localization: string;
  isActive: boolean;
}

const mapLanguage = (lang: APILanguage): Language => ({
  id: lang.id,
  localization: lang.localization,
  name: lang.name,
  isActive: lang.isActive,
})

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  constructor(
    private apiLanguage: APILanguageService
  ) { }

  getLanguages(): Observable<Language[]> {
    return this.apiLanguage.languageGet().pipe(
      map(langs => {
        return langs.map(mapLanguage);
      })
    );
  }

  async setLanguage(id: number): Promise<void> {
    await this.apiLanguage.languageCurrentActiveIdPut({
      id,
    }).toPromise();
  }
}
