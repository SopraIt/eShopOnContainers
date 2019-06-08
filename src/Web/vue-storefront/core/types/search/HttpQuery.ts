export default interface HttpQuery {
  q?: string
  size: number
  from: number
  sort: string
  source?: string,
  _source_excludes?: string
  _source_includes?: string
  source_content_type?: string
}
