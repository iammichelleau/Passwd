import { Config } from './config';

export class ConfigService {
  private apiUrl = 'http://localhost:41177/api/config';

  async submitConfig(config: Config): Promise<any> {
    const response = await fetch(this.apiUrl, {
      method: 'post',
      body: JSON.stringify(config),
      headers: {
        'Content-Type': 'application/json'
      }
    });

    const text = await response.text() + "; Status: " + response.status + " " + response.statusText;
    
    return text;
  }
}

export default new ConfigService();