import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

export interface Country { countryId: number; countryName: string; iso2: string; iso3: string; }
export interface State { stateId: number; stateName: string; stateCode?: string; countryId: number; }
export interface City { cityId: number; cityName: string; stateId: number; countryId: number; }

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private baseUrl = 'https://localhost:7015/api'; // replace with your API base URL

  private countriesCache$: Observable<Country[]> | null = null;
  private statesCache: { [countryId: number]: Observable<State[]> } = {};
  private citiesCache: { [stateId: number]: Observable<City[]> } = {};

  constructor(private http: HttpClient) { }

  // Get all countries (cached)
  getCountries(): Observable<Country[]> {
    if (!this.countriesCache$) {
      this.countriesCache$ = this.http.get<Country[]>(`${this.baseUrl}/Country`)
        .pipe(shareReplay(1)); // cache response
    }
    return this.countriesCache$;
  }

  // Get states by countryId (cached)
  getStates(countryId: number): Observable<State[]> {
    if (!this.statesCache[countryId]) {
      this.statesCache[countryId] = this.http.get<State[]>(`${this.baseUrl}/State/${countryId}`)
        .pipe(shareReplay(1));
    }
    return this.statesCache[countryId];
  }

  // Get cities by stateId (cached)
  getCities(stateId: number): Observable<City[]> {
    if (!this.citiesCache[stateId]) {
      this.citiesCache[stateId] = this.http.get<City[]>(`${this.baseUrl}/City/${stateId}`)
        .pipe(shareReplay(1));
    }
    return this.citiesCache[stateId];
  }

  // Utility: Get all states in a country synchronously if already loaded
  getStatesSync(countryId: number, statesList: State[]): State[] {
    return statesList.filter(s => s.countryId === countryId);
  }

  // Utility: Get all cities in a state synchronously if already loaded
  getCitiesSync(stateId: number, citiesList: City[]): City[] {
    return citiesList.filter(c => c.stateId === stateId);
  }
}
